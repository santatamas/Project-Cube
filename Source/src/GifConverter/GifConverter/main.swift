//
//  main.swift
//  GifConverter
//
//  Created by Tamas Santa on 09/03/16.
//  Copyright © 2016 Tamas Santa. All rights reserved.
//

import Foundation
import AppKit

func PrintHelp()
{
    print("usage: GifConverter source_path target_path");
}

if Process.arguments.count < -2 {
    PrintHelp();
}
else {
    //let source = Process.arguments[0]
    let source = "/Users/thomas/Work/Project-Cube/Source/src/bin/demo.gif"
    //let dest = Process.arguments[1]

    //print("source: " + source)
    //print("destination: " + dest)
    
    print("========= CONVERTING FILE =========")
    print(source)
    print("-----------------------------------")
    
    var data = NSData.init(contentsOfFile: source)
    var imageSource = CGImageSourceCreateWithData(data!, nil)
    
    var frameCount = Int(CGImageSourceGetCount(imageSource!))
    print("No. frames: " + String(frameCount))
    
    var image = NSImage.init(contentsOfFile: source)
    for var frameIndex:Int = 0;frameIndex < frameCount;frameIndex++ {
        print("Processing frame " + String(frameIndex) + " ...")
        let frameImageRef = CGImageSourceCreateImageAtIndex(imageSource!, frameIndex, nil)
        let frameDuration = CGImageSourceGIFFrameDuration(imageSource!, index: frameIndex)
        print("Duration: " + String(frameDuration));
        print("Dumping pixel data...")
        
        let pixelData = CGDataProviderCopyData(CGImageGetDataProvider(frameImageRef))
        let data: UnsafePointer<UInt8> = CFDataGetBytePtr(pixelData)
        
        for var x:Int = 0;x < Int((image?.size.width)!);x++ {
            
            for var y:Int = 0;y < Int((image?.size.width)!);y++ {

                let pixelInfo: Int = ((Int((image?.size.width)!) * Int(y)) + Int(x)) * 4
                
                let r = data[pixelInfo]
                let g = data[pixelInfo+1]
                let b = data[pixelInfo+2]
                let a = data[pixelInfo+3]
                
                print("(R:" + String(r) +
                      " G:" + String(g) +
                      " B:" + String(b) +
                      " A:" + String(a) + ")", terminator:"")
            }
            print("")
        }
        print("")

    }
    
}





