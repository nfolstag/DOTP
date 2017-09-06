using Assets.Codes.DAOs;
using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Assets.Codes.Databases
{
    public class CardsDatabaseManager
    {
        private IDbConnection conn;
        private StringBuilder transformsList;
        private static CardsDatabaseManager instance;

        public CardsDatabaseManager()
        {
            StringBuilder path =
                new StringBuilder("URI=file:")
                .Append(Constants.RESOURCES_ROOT_DIR_FULLPATH)
                .Append(Constants.RESOURCES_ROOT_DB)
                .Append(Constants.RESOURCES_FILE_CARDSDB);
            conn = new SqliteConnection(path.ToString());            
        }

        public static CardsDatabaseManager GetInstance()
        {
            if (instance == null)
                instance = new CardsDatabaseManager();
            return instance;
        }

        public List<Card> GetCards(int numCards = 0, int offset = 0)
        {
            if(transformsList == null)
                SetListOfTransformsCards();
            StringBuilder query = new StringBuilder("SELECT * FROM CARDS WHERE type NOT LIKE '%plane %' AND type NOT LIKE '%scheme%' AND id NOT IN ("+ transformsList + ") ORDER BY name ASC");
            if (numCards > 0)
                query.Append(" LIMIT " + numCards + " ");
            if (offset > 0)
                query.Append(" OFFSET " + offset);

            List<Card> cards = new List<Card>();
            IDbCommand command = Connect().CreateCommand();
            command.CommandText = query.ToString();
            IDataReader reader = command.ExecuteReader();
            StringBuilder cardsIds = new StringBuilder("(");
            while (reader.Read())
            {
                Card card = new Card(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4).Trim(),
                    reader.GetString(5).Trim(),
                    reader.GetString(6),
                    GetCostTable(reader.GetString(7).Trim())
                );
                cards.Add(card);
                cardsIds.Append(card.Id + ",");
            }
            cardsIds.Replace(",", "", cardsIds.Length - 1, 1).Append(")");
            Close(false, reader, command);
            return cards = fillEditionsOfCards(cardsIds, cards);
        }

        private List<Card> fillEditionsOfCards(StringBuilder cardsIds, List<Card> cards)
        {
            StringBuilder query =
                new StringBuilder("SELECT C.id, E.name, E.code1, E.code2 FROM CARDS_EDITIONS AS CE ")
                .Append("INNER JOIN CARDS AS C ON C.name LIKE CE.card_name ")
                .Append("INNER JOIN EDITIONS AS E ON CE.edition_code1 = E.code1 ")
                .Append("WHERE C.id IN ").Append(cardsIds.ToString()).Append(" ORDER BY E.date DESC");
            IDbCommand command = Connect().CreateCommand();
            command.CommandText = query.ToString();
            IDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                cards = addEditionToCardList(
                    cards,
                    new StringBuilder(reader.GetString(2)).Append("-").Append(reader.GetString(3)).ToString(),
                    reader.GetString(1),
                    reader.GetInt32(0)
                );
            }
            Close(false, reader, command);
            return cards;
        }

        public IDbConnection Connect()
        {
            if (conn != null && conn.State == ConnectionState.Closed)
                conn.Open();
            return conn;
        }

        public void Close(bool closeConnection = true, IDataReader reader = null, IDbCommand command = null)
        {
            if (reader != null)
                reader.Close();
            if (command != null)
                command.Dispose();
            if (conn != null && conn.State == ConnectionState.Open && closeConnection)
                conn.Close();
        }

        private Hashtable GetCostTable(string cost)
        {
            Hashtable tblCost = new Hashtable();
            string[] split = cost.Split(' ');
            string preview = null;
            foreach (string c in split)
            {
                if (preview == c)
                    tblCost[c] = (int)tblCost[c] + 1;
                else
                    tblCost.Add(c, 1);
                preview = c;
            }
            return tblCost;
        }

        private List<Card> addEditionToCardList(List<Card> cards, string codes, string name, int id)
        {
            foreach (Card card in cards)
            {
                if (card.Id == id)
                {
                    card.Editions.Add(new string[] { codes, name });
                    break;
                }
            }
            return cards;
        }

        private void SetListOfTransformsCards()
        {
            IDbCommand command = Connect().CreateCommand();
            command.CommandText = "SELECT transform FROM CARDS WHERE transform NOT NULL";
            IDataReader reader = command.ExecuteReader();
            transformsList = new StringBuilder("");
            while (reader.Read())
                transformsList.Append(reader.GetInt32(0)).Append(",");
            transformsList = new StringBuilder(transformsList.ToString().Substring(0, transformsList.Length - 1));
        }

    }
}
