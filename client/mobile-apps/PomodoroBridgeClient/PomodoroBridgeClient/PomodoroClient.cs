using System;
using System.Collections.Generic;
using System.Linq;
using Bridge.Html5;
using Pomodoro;

namespace PomodoroBridgeClient
{
    class PomodoroClient
    {
        private const bool Debug = true;
        private readonly Api _api = new Api(new BridgeNetWebRequester {Logger = Dump});

        private HTMLInputElement _textBoxName;
        private HTMLTableElement _table;

        public void Render()
        {
            var labelWelcome = new HTMLLabelElement
            {
                InnerHTML = "Welcome to Pomodoro!",
                Style =
                {
                    FontSize = "32px",
                    Margin = "10px"
                }
            };

            _textBoxName = new HTMLInputElement
            {
                Placeholder = "Enter name…",
                Value = "PomodoroBridgeClient"
            };

            _table = new HTMLTableElement();
            var buttonGetAll = new HTMLButtonElement
            {
                InnerHTML = "Get all",
                OnClick = ev => RefreshList()
            };

            var buttonStart = new HTMLButtonElement
            {
                InnerHTML = "Start",
                OnClick = ev =>
                {
                    _api.Start(_textBoxName.Value);
                    RefreshList();
                }
            };
            var buttonStop = new HTMLButtonElement
            {
                InnerHTML = "Stop",
                OnClick = ev =>
                {
                    _api.Stop(_textBoxName.Value);
                    RefreshList();
                }
            };

            // Add to the document body
            var htmlBodyElement = Document.Body;
            htmlBodyElement.AppendChild(labelWelcome);
            htmlBodyElement.AppendChild(new HTMLBRElement());

            htmlBodyElement.AppendChild(_textBoxName);
            htmlBodyElement.AppendChild(new HTMLBRElement());

            htmlBodyElement.AppendChild(buttonGetAll);
            htmlBodyElement.AppendChild(buttonStart);
            htmlBodyElement.AppendChild(buttonStop);
            htmlBodyElement.AppendChild(new HTMLBRElement());

            htmlBodyElement.AppendChild(_table);
        }

        private void RefreshList()
        {
            var entries = _api.GetAll();
            FillEntries(entries);
        }

        private void FillEntries(IEnumerable<Api.PomodoroEntry> entries)
        {
            // clear
            foreach (var row in _table.Rows.ToList())
                _table.DeleteRow(row.RowIndex);

            var head = _table.CreateTHead();
            var headRow = head.InsertRow();
            var headCell1 = headRow.InsertCell();
            headCell1.AppendChild(new HTMLLabelElement {InnerHTML = "Name"});
            var headCell2 = headRow.InsertCell();
            headCell2.AppendChild(new HTMLLabelElement {InnerHTML = "Time"});

            var now = DateTime.Now;
            foreach (var entry in entries)
            {
                var labelName = new HTMLLabelElement
                {
                    InnerHTML = entry.Name,
                    OnClick = mouseEvent => _textBoxName.Value = mouseEvent.CurrentTarget.InnerHTML
                };

                var elapsed = now - entry.Time;
                var time = Api.Humanize(elapsed);
                var labelTime = new HTMLLabelElement
                {
                    InnerHTML = time
                };

                var row = _table.InsertRow();
                var cell1 = row.InsertCell();
                cell1.AppendChild(labelName);
                var cell2 = row.InsertCell();
                cell2.AppendChild(labelTime);
            }
        }

        private static void Dump(string msg)
        {
            if (Debug)
                Console.WriteLine(msg);
        }
    }
}