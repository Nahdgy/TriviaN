using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game : AllGames
    {
        private readonly bool[] _inPenaltyBox = new bool[6];

        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();

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
            int mod = _places[_currentPlayer] % _nbCategory(4);
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

                var winner = DidPlayerWin(6);
                NextPlayer();
                return winner;
                
            }
            else
            {
                NextPlayer();
                return true;
            }
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
}
