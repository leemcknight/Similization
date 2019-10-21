using System;
using System.Collections.Generic;
using System.Text;

namespace McKnight.Similization.Server
{
    /// <summary>
    /// Event Arguments for the <i>SpyCaptured</i> event of a <see cref="Country"/>.
    /// </summary>
    public class SpyCapturedEventArgs : System.EventArgs
    {
        private DiplomaticTie diplomaticTie;
        private EspionageAction espionageAction;
        private bool espionageCompleted;
        private City city;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpyCapturedEventArgs"/> class.
        /// </summary>
        /// <param name="diplomaticTie"></param>
        /// <param name="espionageAction"></param>
        /// <param name="espionageCompleted"></param>
        /// <param name="city"></param>
        public SpyCapturedEventArgs(DiplomaticTie diplomaticTie, EspionageAction espionageAction, bool espionageCompleted, City city)
        {
            this.diplomaticTie = diplomaticTie;
            this.espionageCompleted = espionageCompleted;
            this.espionageAction = espionageAction;
            this.city = city;
        }

        /// <summary>
        /// The <see cref="DiplomaticTie"/> between the <see cref="Country"/> that 
        /// captured the spy and the <see cref="Country"/> that lost the spy.
        /// </summary>
        public DiplomaticTie DiplomaticTie
        {
            get { return this.diplomaticTie; }
        }

        /// <summary>
        /// The action that the spy was attempting to perform when (s)he was captured.
        /// </summary>
        public EspionageAction EspionageAction
        {
            get { return this.espionageAction; }
        }

        /// <summary>
        /// Indicates whether the espionage the captured spy was attempting to perform 
        /// was completed.
        /// </summary>
        public bool EspionageCompleted
        {
            get { return this.espionageCompleted; }
        }


        /// <summary>
        /// Indicated the <see cref="City"/> that the spy was captured in.
        /// </summary>
        public City City
        {
            get { return this.city; }
        }
    }
}
