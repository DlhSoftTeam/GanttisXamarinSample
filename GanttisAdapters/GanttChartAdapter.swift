//
//  GanttChartAdapter.swift
//  GanttisAdapters
//
//  Created by DlhSoft on 02/06/2020.
//  Copyright © 2020 DlhSoft. All rights reserved.
//

import UIKit
import GanttisTouch

public class GanttChartAdapter: GanttChart, GanttChartItemObserver {
    @objc public init(items: [Item], dependencies: [Dependency], itemObserver: ItemObserver) {
        super.init(frame: .zero)
        let items = items.map { item in
            GanttChartItem(
                label: item.label, row: item.row,
                start: Time(item.start), finish: Time(item.finish),
                context: item) }
        let dependencies = dependencies.map { dependency in
            GanttChartDependency(
                from: items.first { item in item.context as! Item == dependency.from }!,
                to: items.first { item in item.context as! Item == dependency.to }!,
                context: dependency)
        }
        let itemSource = GanttChartItemSource(items: items, dependencies: dependencies)
        let headerController = GanttChartHeaderController()
        headerController.rows = [
            GanttChartHeaderRow(.weeks),
            GanttChartHeaderRow(.days, format: .dayOfWeekShortAbbreviation)]
        let contentController = GanttChartContentController(itemManager: itemSource)
        contentController.intervalHighlighters = [TimeSelector(.weeks), TimeSelector(.time)]
        contentController.scheduleHighlighters = [ScheduleTimeSelector(.weekends)]
        contentController.timeScale = .intervalsWith(period: 15, in: .minutes)
        let controller = GanttChartController(
            headerController: headerController, contentController: contentController)
        contentController.settings.allowsCreatingBars = false
        contentController.settings.allowsDeletingBars = false
        contentController.settings.allowsResizingCompletionBars = false
        contentController.settings.allowsMovingBarsVertically = false
        contentController.settings.allowsCreatingDependencyLines = false
        contentController.settings.allowsDeletingDependencyLines = false
        itemSource.itemObserver = self
        self.controller = controller
        self.itemObserver = itemObserver
    }
    @objc required dynamic init?(coder aDecoder: NSCoder) {
        fatalError("init(coder:) has not been implemented")
    }
    public weak var itemObserver: ItemObserver?
    public func timeDidChange(for item: GanttChartItem, from originalValue: TimeRange) {
        let start = item.start, finish = item.finish
        let item = item.context as! Item
        item.start = Date(start)
        item.finish = Date(finish)
        itemObserver?.timeDidChange(for: item)
    }
}

@objcMembers public class Item: NSObject {
    public init(label: String, row: Int, start: Date, finish: Date) {
        self.label = label
        self.row = row
        self.start = start
        self.finish = finish
    }
    public internal(set) var label: String
    public internal(set) var row: Int
    public internal(set) var start, finish: Date
}

@objcMembers public class Dependency: NSObject {
    public init(from: Item, to: Item) {
        self.from = from
        self.to = to
    }
    var from, to: Item
}

@objc public protocol ItemObserver {
    func timeDidChange(for item: Item)
}
