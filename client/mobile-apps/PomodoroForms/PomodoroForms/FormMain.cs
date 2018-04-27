using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Pomodoro;

namespace PomodoroForms
{
    public partial class FormMain : Form
    {
        private readonly Api _api = new Api(new NetWebRequester());

        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            var entries = _api.GetAll();
            FillEntries(entries);
            UpdateEntries();

            timerTick.Enabled = true;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            _api.Start(textBoxName.Text);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            _api.Stop(textBoxName.Text);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            _api.Clear();
        }

        private void buttonFlood_Click(object sender, EventArgs e)
        {
            var random = new Random(Environment.TickCount);

            var bigLoad = string.Empty;
            if (checkBoxBigLoad.Checked)
            {
                var load = new string('X', 100000);
                bigLoad = load;
            }

            var name = textBoxName.Text;
            for (var i = 0; i < numericUpDownFlood.Value; i++)
            {
                var randomName = name + "_" + i;

                if (checkBoxScramble.Checked)
                    randomName += random.Next(int.MaxValue);

                randomName += "_" + bigLoad;

                _api.Start(randomName);
            }
        }

        private void listViewPomodoros_SelectedIndexChanged(object sender, EventArgs e)
        {
            var entry = listViewPomodoros.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (entry == null)
                return;
            textBoxName.Text = entry.Text;
        }

        private void timerTick_Tick(object sender, EventArgs e)
        {
            UpdateEntries();
        }

        private void FillEntries(IEnumerable<Api.PomodoroEntry> entries)
        {
            var now = DateTime.UtcNow;
            var items = entries
                .Select(x =>
                {
                    var item = new ListViewItem();
                    item.Text = x.Name;

                    var time = CalculateDuration(now, x.Time);
                    item.SubItems.Add(time);
                    item.Tag = x;
                    return item;
                })
                .ToArray();

            listViewPomodoros.Items.Clear();
            listViewPomodoros.Items.AddRange(items);
        }

        private void UpdateEntries()
        {
            var now = DateTime.UtcNow;
            foreach (ListViewItem item in listViewPomodoros.Items)
            {
                var entry = (Api.PomodoroEntry) item.Tag;
                var newText = CalculateDuration(now, entry.Time);
                var oldText = item.SubItems[1].Text;
                if (newText != oldText)
                    item.SubItems[1].Text = newText;
            }
        }

        private string CalculateDuration(DateTime utcNow, DateTime utcDateTime)
        {
            var timeSpan = utcNow - utcDateTime;
            var humanized = Api.Humanize(timeSpan);
            return humanized;
        }
    }
}