//
//  GameViewController.swift
//  Project Cube
//
//  Created by Tamas Santa on 11/01/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import UIKit
import SpriteKit

class GameViewController: UIViewController {

    @IBOutlet weak var menuButton:UIBarButtonItem!
    
    func LoadScene(gameScene: GameScene)
    {
        if(!gameScene.initialized)
        {
            gameScene.Initialize()
        }
        // Configure the view.
        let skView = self.view as SKView
        skView.showsFPS = true
        skView.showsNodeCount = true
        skView.showsDrawCount = true
        skView.showsQuadCount = true
        skView.showsPhysics = true
        
        /* Sprite Kit applies additional optimizations to improve rendering performance */
        skView.ignoresSiblingOrder = true
        
        skView.presentScene(gameScene)
        
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        if self.revealViewController() != nil {
            menuButton.target = self.revealViewController()
            menuButton.action = "revealToggle:"
            self.navigationController!.navigationBar.addGestureRecognizer(self.revealViewController().panGestureRecognizer())
        }
        
        LoadScene((UIApplication.sharedApplication().delegate! as AppDelegate)._game)
    }

    override func shouldAutorotate() -> Bool {
        return false
    }

    override func supportedInterfaceOrientations() -> Int {
        if UIDevice.currentDevice().userInterfaceIdiom == .Phone {
            return Int(UIInterfaceOrientationMask.AllButUpsideDown.rawValue)
        } else {
            return Int(UIInterfaceOrientationMask.All.rawValue)
        }
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Release any cached data, images, etc that aren't in use.
    }

    override func prefersStatusBarHidden() -> Bool {
        return true
    }
}
