//
//  PetFactory.swift
//  Project Cube
//
//  Created by Tamas Santa on 28/03/16.
//  Copyright © 2016 Tamas Santa. All rights reserved.
//

import Foundation

class PetFactory {
    
    func Create(petType:String) -> Pet
    {
        switch petType {
        case "Kitty":
            return CreateKitty()
        default:
            // fall back to kitty
            return CreateKitty();
        }
    }
    
    private func CreateKitty() -> Pet
    {
        let result:Pet = Pet()
        let codeName = "Kitty"
        result.codeName = codeName
        
        let configPath = NSBundle.mainBundle().pathForResource("config", ofType: "json", inDirectory: codeName)
        let animationLoader = AnimationLoader()
        result.animationDictionary = animationLoader.LoadAnimations(configPath!)
        result.CurrentAnimation = result.GetAnimation("stand")

        return result
    }
    
}