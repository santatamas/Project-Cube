//
//  MenuController.swift
//  Project Cube
//
//  Created by Tamas Santa on 01/03/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation
class MenuController : UITableViewController {
    override func viewDidLoad() {
        super.viewDidLoad()
        
        // Uncomment the following line to preserve selection between presentations
        // self.clearsSelectionOnViewWillAppear = false
        
        // Uncomment the following line to display an Edit button in the navigation bar for this view controller.
        // self.navigationItem.rightBarButtonItem = self.editButtonItem()
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
        if(segue.identifier == "menuitem_pet")
        {
            var pushSegue = segue as SWRevealViewControllerSeguePushController
            //spushSegue.destinationViewController =
        }
    }
}