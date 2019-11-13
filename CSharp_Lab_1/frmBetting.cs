using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace BettingGame
{
    public partial class frmBetting : Form
    {
        public frmBetting()
        {
            InitializeComponent();
        }

        private Punter []_listOfGuys = null;
        private GreyHound []_listOfDogs = null;
        private int _flag = 0;
        private bool _enableRaceBtn = false;
        
        public void FillArrays()
        {
            Random myRandom = new Random();

            _listOfGuys = new Punter[3]
            {
                new Punter() 
                { 
                    Name = "John", 
                    Cash = 50,  
                    MyBet = new Bet(), 
                    MyLabel = Punter1desc, 
                    MyRadioButton = Punter1
                },

                new Punter()
                { 
                    Name = "Jessica", 
                    Cash = 50,  
                    MyBet = new Bet(),  
                    MyLabel = Punter2desc, 
                    MyRadioButton = Punter2
                },

                new Punter() 
                { 
                    Name = "David", 
                    Cash = 50,  
                    MyBet = new Bet(), 
                    MyLabel = Punter3desc, 
                    MyRadioButton = Punter3
                }
            };

            _listOfDogs = new GreyHound[4]
            {
                new GreyHound() 
                { 
                    RaceTrackLength = Track.Width - 70, 
                    StartingPosition = Track.Location.X, 
                    MyRandom = myRandom, 
                    MyPictureBox = Dog1
                },

                new GreyHound()
                { 
                    RaceTrackLength = Track.Width - 70, 
                    StartingPosition = Track.Location.X, 
                    MyRandom = myRandom, 
                    MyPictureBox = Dog2
                },

                new GreyHound() 
                { 
                    RaceTrackLength = Track.Width - 70, 
                    StartingPosition = Track.Location.X, 
                    MyRandom = myRandom, 
                    MyPictureBox = Dog3
                },

                new GreyHound() 
                { 
                    RaceTrackLength = Track.Width - 70, 
                    StartingPosition = Track.Location.X, 
                    MyRandom = myRandom, 
                    MyPictureBox = Dog4
                }
            };

            for (int i = 0; i < _listOfGuys.Length; i++)
            {
                _listOfGuys[i].MyBet.Bettor = _listOfGuys[i];
                _listOfGuys[i].UpdateLabels();
            }

            PlaceDogPicturesAtStart();            
        }

        private void frmBetting_Load(object sender, EventArgs e)
        {
            try
            {
                if (numBucks.Value == 5)
                    MinimumBet.Text = "Minimum limit : 5";

                FillArrays();
                
                if (!this._enableRaceBtn)
                    btnRace.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            maxlbl.Text = "Maxmumum Limit : 15";
        }

        private void Punter1_CheckedChanged(object sender, EventArgs e)
        {
            if (Punter1.Checked)
            {
                this._flag = 1;
                PunterName.Text = this._listOfGuys[0].Name;
            }
        }

        private void Punter2_CheckedChanged(object sender, EventArgs e)
        {
            if (Punter2.Checked)
            {
                this._flag = 2;
                PunterName.Text = this._listOfGuys[1].Name;
            }
        }

        private void Punter3_CheckedChanged(object sender, EventArgs e)
        {
            if (Punter3.Checked)
            {
                this._flag = 3;
                PunterName.Text = this._listOfGuys[2].Name;
            }
        }

        public void BetsBtnWorking()
        {            
            int bucksNumber = 0;
            int dogNumber = 0;

            if (!Punter1.Checked && !Punter2.Checked && !Punter3.Checked)
            {
                MessageBox.Show("You must choose atleast one guy to place bet.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bucksNumber = Convert.ToInt32(numBucks.Value);
            dogNumber = Convert.ToInt32(DogNo.Value);

            if (IsExceedBetLimit(bucksNumber))
            {
                MessageBox.Show("You can't put bucks greater than 15 on dog.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _enableRaceBtn = true; // if at least one bet is placed enable race button then

            if (this._flag == 1)
            {
                this._listOfGuys[0].PlaceBet(bucksNumber, dogNumber);
            }
            else if (this._flag == 2)
            {
                this._listOfGuys[1].PlaceBet(bucksNumber, dogNumber);
            }
            else if (this._flag == 3)
            {
                this._listOfGuys[2].PlaceBet(bucksNumber, dogNumber);
            }            
        }

        private void betbtn_Click(object sender, EventArgs e)
        {
            try
            {
                BetsBtnWorking();

                if (this._enableRaceBtn)
                    btnRace.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public bool IsExceedBetLimit(int amount)
        {
            if (amount > 15 && amount > 5)
                return true;

            return false;
        }

        public void RaceButtonWorking()
        {
            betbtn.Enabled = false;
            btnRace.Enabled = false;

            bool winnerDogFlag = false;
            int winningDogNo = 0;            

            while (!winnerDogFlag)
            {
                for (int i = 0; i < _listOfDogs.Length; i++)
                {
                    if (this._listOfDogs[i].Run())
                    {
                        winnerDogFlag = true;
                        winningDogNo = i;
                    }
                  
                    Track.Refresh();                 
                }                
            }

            MessageBox.Show("We have a winner - dog # " + (winningDogNo + 1) + "!", "Race Over");

            for (int j = 0; j < _listOfGuys.Length; j++)
            {
                this._listOfGuys[j].Collect(winningDogNo + 1);
                this._listOfGuys[j].ClearBet(); // clearing all bets
            }

            PlaceDogPicturesAtStart();

            betbtn.Enabled = true;       
        }

        public void PlaceDogPicturesAtStart()
        {
            for (int k = 0; k < _listOfDogs.Length; k++)
                _listOfDogs[k].TakeStartingPosition();  
        }

        private void btnRace_Click(object sender, EventArgs e)
        {
            try
            {
                RaceButtonWorking();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }        
    }
}
