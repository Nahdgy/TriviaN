using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game : AllGames
    {
        private readonly List<string> _players = new List<string>();

        private readonly int[] _places = new int[6];
        private readonly int _maxPlaces = 11;
        private readonly int[] _purses = new int[6];

        private readonly bool[] _inPenaltyBox = new bool[6];

        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();

        private  int _currentPlayer;
        private readonly int _nbCategory = 4;
        private bool _isGettingOutOfPenaltyBox;

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast(("Science Question " + i));
                _sportsQuestions.AddLast(("Sports Question " + i));
                _rockQuestions.AddLast("Rock Question " + i);
            }
        }

//Verify the minimum Player count.
        public bool IsPlayable()
        {
            return _players.Count >=2;

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
//Decide if a player can get out of the penalty bax during roll in the game;
        public void Roll(int roll)
        {
            Console.WriteLine(_players[_currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_players[_currentPlayer] + " is getting out of the penalty box");

                    ResetPlayerPlace(roll);

                    Console.WriteLine(_players[_currentPlayer]
                            + "'s new location is "
                            + _places[_currentPlayer]);
                    Console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion(CurrentCategory());
                }
                else
                {
                    Console.WriteLine(_players[_currentPlayer] + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                ResetPlayerPlace(roll);

                Console.WriteLine(_players[_currentPlayer]
                        + "'s new location is "
                        + _places[_currentPlayer]);
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion(CurrentCategory());
            }
        }
    //Return player at the 1st places of the game.
        private void ResetPlayerPlace(int roll)
        {
            _places[_currentPlayer] = _places[_currentPlayer] + roll;
            if (_places[_currentPlayer] >= _maxPlaces)
            {
                _places[_currentPlayer] = _places[_currentPlayer] - _maxPlaces;
            }
        }

        //Allow to ask the question depending on the category.
        private void AskQuestion(string category)
        {
            switch(category)
            {
                case "Pop":
                    Console.WriteLine(_popQuestions.First());
                    _popQuestions.RemoveFirst();
                    break;
                case "Science":
                    Console.WriteLine(_scienceQuestions.First());
                    _scienceQuestions.RemoveFirst();
                    break;
                case "Sports":
                    Console.WriteLine(_sportsQuestions.First());
                    _sportsQuestions.RemoveFirst();
                    break;
                case "Rock":
                    Console.WriteLine(_rockQuestions.First());
                    _rockQuestions.RemoveFirst();
                    break;    
            }
        }
        
//Check the current player place and gives him his category.
private string CurrentCategory()
        {
            int mod = _places[_currentPlayer] % _nbCategory;
            switch(mod)
            {
            case 0:
                return "Pop";
            case 1:
                return "Science";
            case 2:
                return "Sports";
            default:
                return "Rock";            
            }
        }

//Gives player purses when he gives a correct answer.
        public bool WasCorrectlyAnswered()
        {
                if (_isGettingOutOfPenaltyBox)
            {
                Console.WriteLine("Answer was correct!!!!");
                _purses[_currentPlayer]++;
                Console.WriteLine(_players[_currentPlayer]
                        + " now has "
                        + _purses[_currentPlayer]
                        + " Gold Coins.");

                var winner = DidPlayerWin();
                NextPlayer();
                return winner;
                
            }
            else
            {
                NextPlayer();
                return true;
            }
        }

        private void NextPlayer()
        {
            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
        }

//Put the player in the penaltybox if he gives a rong answer.
        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            NextPlayer();
            return true;
        }

//Make sure the current player have more than
        private bool DidPlayerWin()
        {
            return !(_purses[_currentPlayer] == 6);
        }
    }

}
