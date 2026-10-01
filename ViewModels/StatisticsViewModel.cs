namespace Projekt.ViewModels
{
    public class StatisticsViewModel : ObservableObject
    {
        public int TodayExitsCount { get; set; }
        public int StudentsCurrentlyOut { get; set; }

        public StatisticsViewModel()
        {
            LoadStats();
        }

        public void LoadStats()
        {
            using var db = new AppDbContext();
            var today = DateTime.Today;

            TodayExitsCount = db.Exits.Count(e => e.ExitTime >= today);
            StudentsCurrentlyOut = db.Students.Count(s => !s.IsInClass);
        }
    }
}
