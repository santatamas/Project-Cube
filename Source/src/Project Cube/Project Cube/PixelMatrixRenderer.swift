//
//  PixelMatrixRenderer.swift
//  Project Cube
//
//  Created by Tamas Santa on 15/01/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation
import SpriteKit

class PixelMatrixRenderer: SKSpriteNode {
    
    var IsInverted: Bool = false
    private let _width: UInt32 = 50
    private let _height: UInt32 = 50
    private var _screenBuffer : [[UInt32]] = [[UInt32]](count: 50, repeatedValue: [UInt32](count: 50, repeatedValue: 0))
    private var _actors: Array<Actor> = []
    private var _totalDelta: Float = 0
    
    func AddActor(actor:Actor)
    {
        _actors.append(actor)
    }
    
    func Act(delta: Float) {
        if(_totalDelta >= 0.100) // frame limiter (calculation)
        {
            ResetBuffer()
            RefreshScreenBuffer(delta)
            self.texture = RenderTexture()
            _totalDelta = 0;
        }
        _totalDelta += delta
    }
    
    func RenderTexture() -> SKTexture
    {
        UIGraphicsBeginImageContext(CGSizeMake(self.frame.width, self.frame.height))
        let ctx:CGContextRef = UIGraphicsGetCurrentContext()!
        
        // Calculate render parameters - should be separated to a one-time init
        let pixelSize: CGFloat = CGFloat((UInt32(self.frame.width) / _width)) // make sure we can round up to 1 pixelspace
        let spaceSize:CGFloat = 0
        let virtualPixelSize: CGFloat = pixelSize + spaceSize
        let offset: CGFloat = CGFloat(UInt32((self.frame.width - (virtualPixelSize * CGFloat(_width))) / 2))
        
        // #DEBUG - DRAW BACKGROUND
        //UIColor(red:255 / 255,green:255 / 255,blue:255 / 255,alpha:1).setFill()
        //CGContextFillRect(ctx, CGRectMake(0,0,self.frame.width, self.frame.height))
        // #END
        
        for var i:Int = 0; i < Int(_width); i++ {
            for var j:Int = 0; j < Int(_height); j++ {
                
                let currentPixelValue = _screenBuffer[i][Int(_height) - 1 - j]
                
                let redComponent = Int((currentPixelValue & 0xFF000000) >> 24)
                let greenComponent = Int((currentPixelValue & 0x00FF0000) >> 16)
                let blueComponent = Int(currentPixelValue & 0x0000FF00 >> 8)
                let alphaComponent = Int((currentPixelValue & 0x000000FF))
                
                UIColor(red:CGFloat(redComponent) / 255,
                        green:CGFloat(greenComponent) / 255,
                        blue:CGFloat(blueComponent) / 255,
                        alpha:CGFloat(alphaComponent) / 255).setFill()

                
                CGContextFillRect(ctx,
                                  CGRectMake(
                                  CGFloat(i) * virtualPixelSize + offset,
                                  CGFloat(j) * virtualPixelSize + offset,
                                  pixelSize,
                                  pixelSize));
            }}
        
        let textureImage:UIImage = UIGraphicsGetImageFromCurrentImageContext()
        UIGraphicsEndImageContext()
        
        let texture:SKTexture = SKTexture(image: textureImage)
        texture.filteringMode = SKTextureFilteringMode.Nearest
        
        return texture
    }
    
    func GetRandomFloat() -> CGFloat{
        let arc4randoMax:Double = 0x100000000
        let upper = 255.0
        let lower = 0.0
        return CGFloat((Double(arc4random()) / arc4randoMax) * (upper - lower) + lower) / 255.0
    }
    
    func RefreshScreenBuffer(delta:Float) {
        var actor: Actor
        
        // do first time load to buffer
        for var i = 0; i < _actors.count;i++
        {
            actor = _actors[i]
            
            // update animation state
            actor.Act(delta)
            
            // get current frame and update buffer
            let frame = actor.GetCurrentFrame()
            
            for var y:UInt32 = 0; y < frame.Height; y++
            {
                for var x:UInt32 = 0; x < frame.Width; x++
                {
                    if(x + actor.Location.X >= _width) {continue}
                    if(x + actor.Location.X < 0) {continue}
                    if(y + actor.Location.Y >= _height) {continue}
                    if(y + actor.Location.Y < 0) {continue}
                    
                    let flippedYaxis = Int(frame.Height - 1 - y)
                    
                    if(frame.Data[Int(x)][flippedYaxis] != 0)
                    {
                        _screenBuffer[Int(x + actor.Location.X)][Int(y + actor.Location.Y)] = frame.Data[Int(x)][flippedYaxis]
                    }
                }
            }
        }
    }
    
    func ResetBuffer() {
        _screenBuffer = [[UInt32]](count: 50, repeatedValue: [UInt32](count: 50, repeatedValue: 0))
    }
}