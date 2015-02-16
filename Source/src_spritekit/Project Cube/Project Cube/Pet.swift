//
//  Pet.swift
//  Project Cube
//
//  Created by Tamas Santa on 15/02/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation

enum LifeState : String {
    case Newborn = "NewBorn"
    case Child = "Child"
    case Adult = "Adult"
}

class Pet : Actor {
    
    var lifeState = LifeState.Newborn
    var codeName:String = ""
    var species:String = ""
    var minHealth = 0
    var maxHealth = 0
    var startHealth = 0
    let serializer = AnimationSerializer()
    
    var animationDictionary = Dictionary<LifeState, Dictionary<String, Animation>>()
    
    func GetAnimation(name:String) -> Animation {
        var animationsForCurrentLifeState = animationDictionary[lifeState]!
        return animationsForCurrentLifeState[name]!
    }
    
    func LoadAnimations(configPath: String)
    {
        // get config file and parse as JSON object
        var err: NSError?
        var data:NSData? = NSFileManager.defaultManager().contentsAtPath(configPath)
        let result = NSJSONSerialization.JSONObjectWithData(data!, options: nil, error: &err) as NSDictionary
        
        // get categories
        let animCats = result["AnimationCategories"] as NSArray
        for (category) in animCats {
            let cat = category as NSDictionary
            var lifeState = LifeState(rawValue: category["@LifeState"] as String)
            var categoryName = category["@Name"] as String
            
            // get animations
            let animations = category["Animations"] as NSArray
            var result = Dictionary<String, Animation>()
            for (animationPhase) in animations {
                let anim = animationPhase as NSDictionary
                var path = anim["@Path"] as String
                var name = anim["@Name"] as String
                
                var directoryPath = path.stringByDeletingLastPathComponent
                var filenameAndExtension = path.lastPathComponent
                var fileName = filenameAndExtension.stringByDeletingPathExtension
                var fileExt = filenameAndExtension.pathExtension
                var bundlePath = NSBundle.mainBundle().pathForResource(fileName, ofType: fileExt, inDirectory: directoryPath)
                
                var unzippedFile = serializer.ReadZip(bundlePath!)
                result[name] = serializer.Deserialize(unzippedFile!)
            }
            
            animationDictionary[lifeState!] = result
        }
    }
}