using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace BettingGame
{
    public class GreyHound
    {        
        private int _startingPosition;

        public int StartingPosition
        {
            get { return _startingPosition; }
            set { _startingPosition = value; }
        }

        private int _raceTrackLength;

        public int RaceTrackLength
        {
            get { return _raceTrackLength; }
            set { _raceTrackLength = value; }
        }

        private int _location;

        public int Location
        {
            get { return _location; }
            set { _location = value; }
        }    

        private PictureBox _myPictureBox;       

        public PictureBox MyPictureBox
        {
            get { return _myPictureBox; }
            set { _myPictureBox = value; }
        }

        private Random _myRandom;

        public Random MyRandom
        {
            get { return _myRandom; }
            set { _myRandom = value; }
        }
       
        public bool Run()
        {
            int randomDistance = this._myRandom.Next(1, 4);
            this._location += randomDistance;

            Point p = this._myPictureBox.Location;

            if (p.X > this._raceTrackLength)
            {
                return true;
            }
            else
            {
                p.X += randomDistance;
                this._myPictureBox.Location = p;

                return false;
            }
        }
  
        public void TakeStartingPosition()
        {                  
            this._location = this._startingPosition;

            Point p = this._myPictureBox.Location;
            p.X = Location;
            this._myPictureBox.Location = p;
        }
    }
}
