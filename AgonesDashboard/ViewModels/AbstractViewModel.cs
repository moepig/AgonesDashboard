using System.Text.Json;

namespace AgonesDashboard.ViewModels
{
    public abstract class AbstractViewModel
    {
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, GetType(), new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
