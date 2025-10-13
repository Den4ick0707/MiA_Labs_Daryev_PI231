using System.Text.Json;
using System.Windows.Forms;

namespace LW_3_4_Daryev1_MiA
{
    class Post
    {
        public string Message { get; set; }
        public string Status { get; set; }
    }

    public partial class MainForm : Form
    {
        private static readonly HttpClient _http = new HttpClient
        {
            BaseAddress = new Uri("https://dog.ceo/api/breeds/"),
            Timeout = TimeSpan.FromSeconds(15)
        };
        public MainForm()
        {
            InitializeComponent();

        }

        public async Task LoadImageFromUrl(string url)
        {
            try
            {
                using var stream = await _http.GetStreamAsync(url);
                dogImage.Image = Image.FromStream(stream);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            try
            {
                var response = await _http.GetAsync("image/random");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                jsonTextDeserialiser.Text = json.ToString();

                var post = JsonSerializer.Deserialize<Post>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (post != null && post.Status == "success")
                {
                    urlTB.Text = post.Message;
                    await LoadImageFromUrl(post.Message);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"HTTP error: {ex.Message}");
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("Request timed out.");
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"JSON parse error: {ex.Message}");
            }
        }
    }
}

