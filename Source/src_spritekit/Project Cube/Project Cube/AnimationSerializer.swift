//
//  AnimationSerializer.swift
//  Project Cube
//
//  Created by Tamas Santa on 11/01/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation


class AnimationSerializer {
    
    func ReadZip(){
        
        var filePath:NSString = NSString(string: "")
        var data:NSData? = NSFileManager.defaultManager().contentsAtPath(filePath)
        var unzippedData:NSData? = data?.gunzippedData()
        
        
    }
    
    func Deserialize(animationData: NSData) -> Animation {
    
        var binaryScanner: BinaryDataScanner = BinaryDataScanner(data: animationData, littleEndian: true, encoding: NSStringEncoding() )
        var result: Animation = Animation()
        
        // Get File version - 1 byte
        var version: Byte = binaryScanner.readByte()!
        //TODO: check for supported file versions
        
        // Get number of frames - 2 bytes
        var noFrames:UInt16 = binaryScanner.read16()!
        
        // Get the ColorDepth of the frames in animation - 1 byte
        var colorDepthValue: Byte = binaryScanner.readByte()!
        if(colorDepthValue == 1)
        {
            result.Depth = ColorDepth.OneBit
        }
        if(colorDepthValue == 8)
        {
            result.Depth = ColorDepth.GrayScale
        }
        
        var frame:Frame
        
        var data: [[Int8]]
        // Iterate through the frames and add them to Animation

        for var frameIndex:UInt16 = 0;frameIndex < noFrames;frameIndex++ {
            
            // Get the duration - 2 byte
            var duration:UInt16 = binaryScanner.read16()!
            
            // Get the width and height
            var width:UInt16 = binaryScanner.read16()!
            var height:UInt16 = binaryScanner.read16()!
            
            frame = Frame()
            frame.Width = Int16(width)
            frame.Height = Int16(height)
            frame.Duration = Int16(duration)
            frame.Depth = result.Depth
            
            // Fill pixel data
            data = [[]]
            for var i:Int = 0;i < Int(frame.Width); i++ {
                for var j:Int = 0;j < Int(frame.Height); j++ {
                    data[i][j] = Int8(binaryScanner.readByte()!)
                }
            }
            frame.Data = data
            result.Frames.append(frame)
        }
        return result
    }
}
