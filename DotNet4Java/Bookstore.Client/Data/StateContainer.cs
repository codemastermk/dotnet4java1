namespace Bookstore.Client.Data
{
    public class StateContainer
    {
        private string? _savedString;

        public string Property
        {
            get => _savedString ?? string.Empty;
            set
            {
                _savedString = value;
                NotifyStateChages();
            }
        }

        public event Action? OnChange;

        private void NotifyStateChages() => OnChange?.Invoke();
    }
}
