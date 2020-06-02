using System;

using ObjCRuntime;
using Foundation;
using UIKit;

namespace GanttisAdapters
{
    [BaseType(typeof(UIView), Name = "_TtC15GanttisAdapters17GanttChartAdapter"), Protocol]
    interface GanttChart {
        [Export("initWithItems:dependencies:itemObserver:")]
        IntPtr Constructor(GanttChartItem[] items, GanttChartDependency[] dependencies, IGanttChartItemObserver itemObserver);
    }

    [BaseType(typeof(UIView), Name = "_TtC15GanttisAdapters4Item"), Protocol]
    interface GanttChartItem
    {
        [Export("initWithLabel:row:start:finish:")]
        IntPtr Constructor(string label, int row, NSDate start, NSDate finish);

        [Export("label")]
        string Label { get; set; }

        [Export("row")]
        int Row { get; set; }

        [Export("start")]
        NSDate Start { get; set; }

        [Export("finish")]
        NSDate Finish { get; set; }
    }

    [BaseType(typeof(UIView), Name = "_TtC15GanttisAdapters10Dependency"), Protocol]
    interface GanttChartDependency
    {
        [Export("initFrom:to:")]
        IntPtr Constructor(GanttChartItem from, GanttChartItem to);
    }

    interface IGanttChartItemObserver { }

    [BaseType(typeof(NSObject), Name = "_TtP15GanttisAdapters12ItemObserver_"), Protocol]
    interface GanttChartItemObserver
    {
        [Export("timeDidChangeFor:")]
        void TimeDidChangeFor(GanttChartItem item);
    }

    [BaseType(typeof(NSObject), Name = "_TtC15GanttisAdapters14GanttisLicense"), Protocol]
    interface License { }
}