using WebAdminConsole.Models;

namespace WebAdminConsole.ViewModels
{
    public class RecordCheckViewModel
    {
        public Stage? Stage { get; set; }

        public Category Category { get; set; }

        public bool Overall { get; set; }

        public TimeSpan? CategoryRecord { get; set; }

        public TimeSpan? NewTime { get; set; }

        public Runner Runner { get; set; }

    }
}
