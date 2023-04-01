using System;

namespace Trivia
{ 
	public class AllGames
	{
        protected List<string> _players = new List<string>();

        protected int[] _places = new int[6];
        protected int _maxPlaces = 11;
        protected int[] _purses = new int[6];

        protected int _currentPlayer;
        protected int _nbCategory = 4;
        protected int _maxPoints = 0;

        //Verify the minimum Player count.
        public bool IsPlayable()
        {
            return _players.Count >= 2;

        }
        //Return player at the 1st places of the game.
        private void ResetPlayerPlace(int set)
        {
            _places[_currentPlayer] = _places[_currentPlayer] + set;
            if (_places[_currentPlayer] >= _maxPlaces)
            {
                _places[_currentPlayer] = _places[_currentPlayer] - _maxPlaces;
            }
        }
        //Pass to the next player
        private void NextPlayer()
        {
            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
        }

        //Initialize player name, purses and make sure he's out of the PenaltyBox.
        public bool Add(string playerName)
        {
            _players.Add(playerName);

            int howManyPlayers = _players.Count;

            _places[howManyPlayers] = 0;
            _purses[howManyPlayers] = 0;
            _inPenaltyBox[howManyPlayers] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
            return true;
        }
        //Make sure the current player have the max points
        private bool DidPlayerWin(int _maxPoints)
        {
            return !(_purses[_currentPlayer] == _maxPoints);
        }
    }
}
}