using Foundation;
using System;
using UIKit;

using GanttisAdapters;

namespace GanttisXamarinSample
{
    public partial class ViewController : UIViewController, IGanttChartItemObserver
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            var today = DateTime.UtcNow.Date;
            var items = new[]
            {
                new GanttChartItem(label: "Task 1", row: 0, start: (NSDate)today, finish: (NSDate)today.AddDays(1)),
                new GanttChartItem(label: "Task 2", row: 1, start: (NSDate)today, finish: (NSDate)today.AddDays(2)),
                new GanttChartItem(label: "Task 3", row: 2, start: (NSDate)today.AddDays(3), finish: (NSDate)today.AddDays(7))
            };
            var dependencies = new[]
            {
                new GanttChartDependency(from: items[1], to: items[2])
            };
            var ganttChart = new GanttChart(items, dependencies, itemObserver: this);
            ganttChart.TranslatesAutoresizingMaskIntoConstraints = false;
            View.AddSubview(ganttChart);
            NSLayoutConstraint.ActivateConstraints(new[]
            {
                ganttChart.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
                ganttChart.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
                ganttChart.TopAnchor.ConstraintEqualTo(View.TopAnchor),
                ganttChart.BottomAnchor.ConstraintEqualTo(View.BottomAnchor)
            });
        }

        [Export("timeDidChangeFor:")]
        void TimeDidChangeFor(GanttChartItem item)
        {
            Console.WriteLine($"Time has changed for {item.Label} (at row {item.Row}) to: {item.Start}-{item.Finish}");
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}