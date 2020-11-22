using System;

namespace FieldFactory.Core.Entities.Verbs
{
    public class Provide
    {
        #region Members
        public string IdPlayer { get; set; }
        public string IdFurniture { get; set; }
        public int NewLevel { get; set; }
        /// <summary>
        /// Returns friendly string date format for serialization
        /// </summary>
        public string DateEnd
        {
            get
            {
                return m_dateEnd.ToString("yyyy/MM/dd HH:mm:ss");
            }
        }

        #endregion

        #region Getters&Setters

        private DateTime m_dateEnd;
        //Getter et Setter with DateTime
        public void SetDateEnd(DateTime d)
        {
            m_dateEnd = d;
        }

        public DateTime GetDateEnd()
        {
            return m_dateEnd;
        }


        #endregion
    }
}
