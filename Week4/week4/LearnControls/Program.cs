namespace LearnControls
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Form1 f = new Form1();
            f.Text = "Hello World";
            f.Size = new Size(800, 600);
            //f.Opacity = 0.75;
            f.BackgroundImage = Image.FromFile("stack-9.jpg");
            f.BackgroundImageLayout = ImageLayout.Center;

            frmCalculator calc = new frmCalculator();


            Application.Run(calc);
        }
    }
}