//
//  Licensing.swift
//  GanttisAdapters
//
//  Created by DlhSoft on 02/06/2020.
//  Copyright © 2020 DlhSoft. All rights reserved.
//

import Foundation
import GanttisTouch

public class GanttisLicense: NSObject {
    /// To be called upon AppDelegate initialization.
    public override init() {
        GanttisTouch.license = ""
    }
}
