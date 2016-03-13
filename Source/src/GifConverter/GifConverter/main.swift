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
    let dest = "/Users/thomas/Work/Project-Cube/Source/src/bin/demo.pma"
    //let dest = Process.arguments[1]

    //print("source: " + source)
    //print("destination: " + dest)
    
    var processor = GifProcessor()
    var serializer = AnimationSerializer()
    
    print("========= CONVERTING FILE =========")
    print(source)
    print("-----------------------------------")
    var data = NSData.init(contentsOfFile: source)
    var animation = processor.getAnimation(data!)
    var binaryAnimationData = serializer.Serialize(animation)
    let compressedData : NSData = try! data!.gzippedData()

    compressedData.writeToFile(dest, atomically: true)
    print("-----------------------------------")
    print("Conversion Finished.")
}





