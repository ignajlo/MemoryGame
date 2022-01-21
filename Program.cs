using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Threading;
class Program
{
    static void Main(string[] args)
    {
        //random number
        Random rnd = new Random();
        //Stopwatch
        Stopwatch stopwatch = new Stopwatch();

        ///open words.txt
        string[] lines = System.IO.File.ReadAllLines(@"Words.txt");

        
        

        //cheks if the game is over
        bool endOfGame = false;

        //mainGameplay
        bool gameplayCheck = true;
        bool won = false;

        //number of chances
        int numberOfChances = 0;
        int chancesUsed = 0;

        //which row
        int row = 1;


        //checks game difficulty
        bool difficultyHard = false;
        string difficultyLevel = " ";

        //checks if the input is correct
        bool inputCorrect = false;


        //input
        string input = "";



        //arrays of strings for the game

        string[] toGuess1 = new string[8];
        string[] toGuess2 = new string[8];

        string[] numbers = new string[8];
        string[] words1 = new string[8];
        string[] words2 = new string[8];

        string[] guessed1 = new string[8];
        string[] guessed2 = new string[8];



        //chosen words
        string word1 = "123123123123";
        string word2 = "321321321321 ";
        int word1Index = 0;
        int word2Index = 0;

        //checks if both words were displayed
        int displayed = 0;


        //number of guesseds words
        int numberOfGuessed = 0;

        using StreamWriter file = new("HighScore.txt", append: true);


        Console.WriteLine("Welcome to Memory Game");


        //GameLoop
        while (!endOfGame)
        {
            won = false;


            numberOfGuessed = 0;
            Console.Clear();
            //Asks for difficulty lvl
            inputCorrect = false;
            while (!inputCorrect)
            {
                Console.WriteLine("\nChoose difficulty(H = hard, E = Easy): ");
                input = Console.ReadLine();

                if (input == "H")
                {
                    difficultyHard = true;
                    numberOfChances = 15;
                    inputCorrect = true;
                    difficultyLevel = "hard";
                    for(int i = 0; i < 8; i++)
                    {
                        guessed1[i] = guessed2[i] = "X";
                        numbers[i] = (i+1).ToString();
                        words1[i] = words2[i] = "X";
                    }
                    //reading random lines from file
                    for (int i = 0; i < 8; i++)
                    {
                        toGuess1[i] = lines[rnd.Next(0,100)];
                        //toGuess2[i] = toGuess1[i];
                        for(int j = 0; j < i; j++)
                        {
                            if(toGuess1[j] == toGuess1[i])
                            {
                                i--;
                            }
                        }
                    }
                    
                }
                else if (input == "E")
                {
                    difficultyHard = false;
                    numberOfChances = 10;
                    inputCorrect = true;
                    difficultyLevel = "easy";
                    for (int i = 0; i < 5; i++)
                    {
                        guessed1[i] = guessed2[i] = "X";
                        numbers[i] = (i+1).ToString();
                        words1[i] = words2[i] = "X";
                    }
                    for (int i = 4; i < 8; i++)
                    {
                        guessed1[i] = guessed2[i] = " ";
                        toGuess1[i] = " ";
                        numbers[i] = " ";
                        words1[i] = words2[i] = " ";
                    }

                    //reading random lines from file
                    for (int i = 0; i < 4; i++)
                    {
                        toGuess1[i] = lines[rnd.Next(0, 100)];
                        //toGuess2[i] = toGuess1[i];
                        for (int j = 0; j < i; j++)
                        {
                            if (toGuess1[j] == toGuess1[i])
                            {
                                i--;
                            }
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Wrong input, try again");
                    inputCorrect = false;
                }
            }

            toGuess2 = toGuess1.OrderBy(x => rnd.Next()).ToArray();
            if(!difficultyHard)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (toGuess2[i] == " ")
                    {
                        for (int j = 4; j < 8; j++)
                        {
                            if (toGuess2[j] != " ")
                            {
                                (toGuess2[i], toGuess2[j]) = (toGuess2[j], toGuess2[i]);
                            }
                        }


                    }
                }
            }
            

            Console.Clear();
            stopwatch.Reset();
            stopwatch.Start();

            //main gameplay
            gameplayCheck = true;
            while (gameplayCheck)
            {



                




                Console.Clear();
                
                //display matrix
                Console.WriteLine("-------------------------------------------------------------\n");
                Console.WriteLine("   Level:" + difficultyLevel + "\n");
                Console.WriteLine("   Guess chances:" + numberOfChances + "\n");
                Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}{7,-15}{8,-15}", " ", numbers[0], numbers[1], numbers[2], numbers[3], numbers[4], numbers[5], numbers[6], numbers[7]);
                Console.WriteLine("\n");
                Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}{7,-15}{8,-15}", "A", words1[0], words1[1], words1[2], words1[3], words1[4], words1[5], words1[6], words1[7]);
                Console.WriteLine("\n");
                Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}{7,-15}{8,-15}", "B", words2[0], words2[1], words2[2], words2[3], words2[4], words2[5], words2[6], words2[7]);
                Console.WriteLine("\n\n-------------------------------------------------------------\n\n");

                //checks if won
                if (displayed == 1)
                {
                    if (difficultyHard && numberOfGuessed == 8)
                    {
                        gameplayCheck = false;
                        won = true;

                    }
                    else if (! difficultyHard && numberOfGuessed == 4)
                    {
                        gameplayCheck = false;
                        won = true;
                    }
                }

                //asks which word to uncover
                inputCorrect = false;
                while (!inputCorrect && row < 3 && !won)
                {
                    if (row == 1)
                    {
                        Console.WriteLine("Which word do you want to uncover in the row A?: ");
                        input = Console.ReadLine();

                        if (difficultyHard && input != "1" && input != "2" && input != "3" && input != "4" && input != "5" && input != "6" && input != "7" && input != "8")
                        {
                            Console.Clear();
                            Console.WriteLine("Wrong input, try again");
                            inputCorrect = false;
                        }
                        else if (!difficultyHard && input != "1" && input != "2" && input != "3" && input != "4")
                        {
                            Console.Clear();
                            Console.WriteLine("Wrong input, try again");
                            inputCorrect = false;
                        }
                        else if(guessed1[Convert.ToInt32(input) - 1] == toGuess1[Convert.ToInt32(input) - 1])
                        {
                            Console.Clear();
                            Console.WriteLine("Wrong input, try again");
                            inputCorrect = false;
                        }
                        else
                        {
                            displayed = 0;
                            word1Index = Convert.ToInt32(input) - 1;
                            word1 = words1[word1Index] = toGuess1[word1Index];
                            inputCorrect = true;
                            row++;
                        }
                    }
                    else if (row == 2)
                    {
                        Console.WriteLine("Which word do you want to uncover in the row B?: ");
                        input = Console.ReadLine();

                        if (difficultyHard && input != "1" && input != "2" && input != "3" && input != "4" && input != "5" && input != "6" && input != "7" && input != "8")
                        {
                            Console.Clear();
                            Console.WriteLine("Wrong input, try again");
                            inputCorrect = false;
                        }
                        else if (!difficultyHard && input != "1" && input != "2" && input != "3" && input != "4")
                        {
                            Console.Clear();
                            Console.WriteLine("Wrong input, try again");
                            inputCorrect = false;
                        }
                        else if (guessed2[Convert.ToInt32(input) - 1] == toGuess2[Convert.ToInt32(input) - 1])
                        {
                            Console.Clear();
                            Console.WriteLine("Wrong input, try again");
                            inputCorrect = false;
                        }
                        else
                        {
                            word2Index = Convert.ToInt32(input) - 1;
                            word2 = words2[word2Index] = toGuess2[word2Index];
                            inputCorrect = true;
                            row++;
                           
                        }
                    }

                    


                }


                //allows second row to display
                if (row == 3 && displayed != 3)
                {
                    displayed++;
                }
                if(displayed==3)
                {
                    row++;
                }


                

                



                //checks if words are the same
                if (word2 == word1)
                {
                    guessed1[word1Index] = toGuess1[word1Index];
                    guessed2[word2Index] = toGuess2[word2Index];
                    numberOfGuessed ++;
                    row = 6;
                }
                else if (row > 4)
                {
                    Console.WriteLine("Words don't match, press enter to continue");
                    numberOfChances--;
                    Console.ReadLine();
                    row++;
                }

                if (row > 5)
                {
                    row = 1;
                    guessed1.CopyTo(words1, 0);
                    guessed2.CopyTo(words2, 0);
                    word1 = "123123123123";
                    word2 = "321321321321";
                }

                //checks if won
                               


                if (displayed == 3)
                {
                    

                    if (numberOfChances == 0)
                    {
                        won = false;
                        gameplayCheck = false;
                    }
                }











                //gameplayCheck = false;
            }

            stopwatch.Stop();

            if(difficultyHard)
            {
                chancesUsed = 15 - numberOfChances;
            }
            else
            {
                chancesUsed = 10 - numberOfChances;
            }


            if(won)
            {
                Console.WriteLine("YOU WON!\n");
                Console.WriteLine("Statistics:");
                Console.WriteLine("Time: {0} s", stopwatch.Elapsed.Seconds);
                Console.WriteLine("Chances used: {0}", chancesUsed);

                Console.WriteLine("What is your name?:");
                input = Console.ReadLine();

                file.WriteLineAsync(input + " | " + DateTime.Now + " | " + stopwatch.Elapsed.Seconds + " seconds" + " | " + chancesUsed + " chances used" + " |");

            }
            else
            {
                Console.WriteLine("YOU LOST :( \n");
            }


            inputCorrect = false;
            while(!inputCorrect)
            {
                Console.WriteLine("Do you want to play again?(Y or n): ");
                input = Console.ReadLine();

                if(input == "Y")
                {
                    endOfGame = false;
                    inputCorrect = true;
                }
                else if(input == "n")
                {
                    endOfGame = true;
                    inputCorrect = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Wrong input, try again");
                    inputCorrect = false;
                }
            }
        }

    }
}
