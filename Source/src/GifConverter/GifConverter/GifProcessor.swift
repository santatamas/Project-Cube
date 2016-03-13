//
//  GifProcessor.swift
//  GifConverter
//
//  Created by Tamas Santa on 12/03/16.
//  Copyright © 2016 Tamas Santa. All rights reserved.
//

import Foundation
import AppKit

class GifProcessor {
    
    func getAnimation(gifData: NSData) -> Animation {
        let result = Animation()
        
        let imageSource = CGImageSourceCreateWithData(gifData, nil)
        let frameCount = Int(CGImageSourceGetCount(imageSource!))
        print("No. frames: " + String(frameCount))
        
        let image = NSImage.init(data: gifData)
        let width = Int((image?.size.width)!)
        let height = Int((image?.size.height)!)
        print("Size: " + String(width) + " X " + String(height))
        
        var frame:Frame
        var frameData:[[UInt32]]
        for var frameIndex:Int = 0;frameIndex < frameCount;frameIndex++ {
            print("Processing frame " + String(frameIndex) + " ...")
            let frameImageRef = CGImageSourceCreateImageAtIndex(imageSource!, frameIndex, nil)
            let frameDuration = CGImageSourceGIFFrameDuration(imageSource!, index: frameIndex)
            print("Duration: " + String(frameDuration));
            
            frame = Frame()
            frame.Duration = UInt32(frameDuration)
            frame.Width = UInt32(width)
            frame.Height = UInt32(height)
            
            print("Dumping pixel data...")
            frameData = [[UInt32]](count: Int(width), repeatedValue: [UInt32](count: Int(height), repeatedValue: 0))
            let pixelData = CGDataProviderCopyData(CGImageGetDataProvider(frameImageRef))
            let pixelDataPointer: UnsafePointer<UInt8> = CFDataGetBytePtr(pixelData)
            
            var tempPixel: UInt32 = 0
            var pixelColors : [UInt8] = [0, 0, 0, 0]
            for var x:Int = 0;x < width;x++ {
                
                for var y:Int = 0;y < height;y++ {
                    let pixelInfo = ((width * y) + x) * 4
                    
                    pixelColors[0] = pixelDataPointer[pixelInfo]
                    pixelColors[1] = pixelDataPointer[pixelInfo+1]
                    pixelColors[2] = pixelDataPointer[pixelInfo+2]
                    pixelColors[3] = pixelDataPointer[pixelInfo+3]
                    
                    // I don't even know that this wizardry is...
                    tempPixel = pixelColors.withUnsafeBufferPointer({
                        UnsafePointer<UInt32>($0.baseAddress).memory
                    })
                    tempPixel = UInt32(bigEndian: tempPixel)
                    frameData[x][y] = tempPixel
                }
            }
            
            frame.Data = frameData
            result.Frames.append(frame)
        }
        return result;
    }
}