//
//  AchievementsTableController.swift
//  Project Cube
//
//  Created by Tamas Santa on 01/03/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation
import UIKit

class AchievementsTableViewController: UITableViewController {
    @IBOutlet weak var menuButton:UIBarButtonItem!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        if self.revealViewController() != nil {
            menuButton.target = self.revealViewController()
            menuButton.action = "revealToggle:"
            self.navigationController!.navigationBar.addGestureRecognizer(self.revealViewController().panGestureRecognizer())
        }

        
        // Uncomment the following line to preserve selection between presentations
        // self.clearsSelectionOnViewWillAppear = false
        
        // Uncomment the following line to display an Edit button in the navigation bar for this view controller.
        // self.navigationItem.rightBarButtonItem = self.editButtonItem()
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    // MARK: - Table view data source
    
    override func numberOfSectionsInTableView(tableView: UITableView) -> Int {
        // Return the number of sections.
        return 1
    }
    
    override func tableView(tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        // Return the number of rows in the section.
        return 3
    }
    
    
    override func tableView(tableView: UITableView, cellForRowAtIndexPath indexPath: NSIndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCellWithIdentifier("Cell", forIndexPath: indexPath) as AchievementTableViewCell
        
        // Configure the cell...
        if indexPath.row == 0 {
            //cell.postImageView.image = UIImage(named: "watchkit-intro")
            cell.postTitleLabel.text = "WatchKit Introduction: Building a Simple Guess Game"
            cell.authorLabel.text = "Simon Ng"
            //cell.authorImageView.image = UIImage(named: "author")
            
        } else if indexPath.row == 1 {
            //cell.postImageView.image = UIImage(named: "custom-segue-featured-1024")
            cell.postTitleLabel.text = "Building a Chat App in Swift Using Multipeer Connectivity Framework"
            cell.authorLabel.text = "Gabriel Theodoropoulos"
            //cell.authorImageView.image = UIImage(named: "appcoda-300")
            
        } else {
            //cell.postImageView.image = UIImage(named: "webkit-featured")
            cell.postTitleLabel.text = "A Beginner’s Guide to Animated Custom Segues in iOS 8"
            cell.authorLabel.text = "Gabriel Theodoropoulos"
            //cell.authorImageView.image = UIImage(named: "appcoda-300")
            
        }
        
        return cell
    }
}
