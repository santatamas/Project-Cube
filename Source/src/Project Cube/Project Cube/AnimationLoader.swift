//
//  AnimationLoader.swift
//  Project Cube
//
//  Created by Tamas Santa on 28/03/16.
//  Copyright © 2016 Tamas Santa. All rights reserved.
//

import Foundation

class AnimationLoader {
    
    let serializer = AnimationSerializer()
    
    
    func LoadAnimations(configPath: String) -> Dictionary<LifeState, Dictionary<String, Animation>>
    {
        var animationDictionary = Dictionary<LifeState, Dictionary<String, Animation>>()

        // get config file and parse as JSON object
        let data:NSData? = NSFileManager.defaultManager().contentsAtPath(configPath)
        
        do {
            let result = try NSJSONSerialization.JSONObjectWithData(data!, options: []) as! NSDictionary
            
            
            // get categories
            let animCats = result["AnimationCategories"] as! NSArray
            for (category) in animCats {
                let cat = category as! NSDictionary
                let lifeState = LifeState(rawValue: (cat["@LifeState"] as! String))
                //var categoryName = (cat["@Name"] as! String)
                
                // get animations
                let animations = category["Animations"] as! NSArray
                var result = Dictionary<String, Animation>()
                for (animationPhase) in animations {
                    let anim = animationPhase as! NSDictionary
                    let path = anim["@Path"] as! String
                    let name = anim["@Name"] as! String
                    
                    let directoryPath = path.stringByDeletingLastPathComponent
                    let filenameAndExtension = path.lastPathComponent
                    let fileName = filenameAndExtension.stringByDeletingPathExtension
                    let fileExt = filenameAndExtension.pathExtension
                    let bundlePath = NSBundle.mainBundle().pathForResource(fileName, ofType: fileExt, inDirectory: directoryPath)
                    
                    let unzippedFile = serializer.ReadZip(bundlePath!)
                    result[name] = serializer.Deserialize(unzippedFile!)
                }
                
                animationDictionary[lifeState!] = result
            }
        } catch {
            
        }
        return animationDictionary
    }
    
}