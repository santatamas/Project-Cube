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
    print("usage: GifConverter source_path");
}

func convertFile(source:String, dest:String) {
    let processor = GifProcessor()
    let serializer = AnimationSerializer()
    
    print(source)
    print("-----------------------------------")
    
    let data = NSData.init(contentsOfFile: source)
    let animation = processor.getAnimation(data!)
    let binaryAnimationData = serializer.Serialize(animation)
    let compressedData : NSData = try! binaryAnimationData!.gzippedData()
    
    compressedData.writeToFile(dest, atomically: true)
    print("-----------------------------------")

}

if Process.arguments.count < -2 {
    PrintHelp();
}
else {
    //let source = Process.arguments[0]
    let source = "/Users/thomas/Work/Project-Cube/Source/src/bin/"
    
    // Create result directory
    do {
        try NSFileManager.defaultManager().createDirectoryAtPath(source + "result/", withIntermediateDirectories: false, attributes: nil)
    } catch let error as NSError {
        print(error.localizedDescription);
    }
    
    // Iterate through GIFs, put the converter result into -> /result/
    let fileManager = NSFileManager.defaultManager()
    let enumerator:NSDirectoryEnumerator = fileManager.enumeratorAtPath(source)!
    
    print("========= CONVERTING FILE(S) =========")
    while let element = enumerator.nextObject() as? String {
        if element.hasSuffix("gif") {
            let destUrl = source + "result/" + (NSURL(fileURLWithPath: element).URLByDeletingPathExtension?.lastPathComponent!)! + ".pmz"
            convertFile(source + element, dest: destUrl)
        }
    }
    print("Conversion Finished.")
}






