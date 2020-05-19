namespace Checkers
{
    public class Program
    {
        /**
         * Program entry point.
         * The program uses images taken from the website opengameart.org.
         */
        public static void Main()
        {
            GameUI gameUI = GameUI.GetInstance();
            gameUI.NewGame();
        }
    }
}