//
//  AnimationSerializer.swift
//  Project Cube
//
//  Created by Tamas Santa on 11/01/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation


class AnimationSerializer {
    
    func ReadZip(path:String) -> NSData? {
        let data = NSData.init(contentsOfFile: path)
        return try! data!.gunzippedData()
    }
    
    func Serialize(animation:Animation) -> NSData? {

        let data = NSMutableData()
        
        // Write File version - 1 byte
        var version: UInt8 = 1
        data.appendBytes(&version, length:sizeof(UInt8))
        
        // Write number of frames - 4 bytes
        var noFrames: Int32 = Int32(animation.Frames.count)
        data.appendBytes(&noFrames, length:sizeof(Int))
        
        // Write out frames
        for var frameIndex:Int32 = 0;frameIndex < noFrames;frameIndex++ {
            let currentFrame = animation.Frames[Int(frameIndex)]
            
            // duration - 4 byte
            var duration = currentFrame.Duration
            data.appendBytes(&duration, length:sizeof(UInt32))
            
            // width and height - 4 + 4 bytes
            var width = currentFrame.Width
            data.appendBytes(&width, length:sizeof(UInt32))
            var height = currentFrame.Height
            data.appendBytes(&height, length:sizeof(UInt32))
            
            // Write pixel data
            var temp: UInt32 = 0
            for var i:Int = 0;i < Int(width); i++ {
                for var j:Int = 0;j < Int(height); j++ {
                    temp = currentFrame.Data[i][j]
                    data.appendBytes(&temp, length:sizeof(Int32))
                }
            }
        }
        return data;
    }
    
    func Deserialize(animationData: NSData) -> Animation {
    
        let binaryScanner: BinaryDataScanner = BinaryDataScanner(data: animationData, littleEndian: true, encoding: NSStringEncoding() )
        let result: Animation = Animation()
        
        // Get File version - 1 byte
        let version: UInt8 = binaryScanner.readByte()!
        
        // Get number of frames - 4 bytes
        let noFrames:Int32 = Int32(binaryScanner.read32()!)
        
        var frame:Frame
        
        var data: [[UInt32]]
        // Iterate through the frames and add them to Animation

        for var frameIndex:Int32 = 0;frameIndex < noFrames;frameIndex++ {
            
            // Get the duration - 4 byte
            let duration = binaryScanner.read32()!
            
            // Get the width and height
            let width = binaryScanner.read32()!
            let height = binaryScanner.read32()!
            
            frame = Frame()
            frame.Width = width
            frame.Height = height
            frame.Duration = duration
            
            // Fill pixel data
            data = [[UInt32]](count: Int(width), repeatedValue: [UInt32](count: Int(height), repeatedValue: 0))
            
            for var i:Int = 0;i < Int(frame.Width); i++ {
                for var j:Int = 0;j < Int(frame.Height); j++ {
                    let temp: UInt32? = binaryScanner.read32()!
                    data[i][j] = temp!
                }
            }
            frame.Data = data
            result.Frames.append(frame)
        }
        return result
    }
}