namespace DataManager
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
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (_, e) =>
                MessageBox.Show(e.Exception.ToString(), "UI Exception");
            AppDomain.CurrentDomain.UnhandledException += (_, e) =>
                MessageBox.Show(e.ExceptionObject?.ToString() ?? "Unknown exception", "Unhandled Exception");

            try
            {
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Startup Exception");
            }
        }
    }
}
