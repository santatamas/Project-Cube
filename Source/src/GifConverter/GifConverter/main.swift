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
    print("usage: GifConverter source_path dest_path");
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
    print("Verifying...")
    var dataToVerify = serializer.ReadZip(dest)
    var verifiedResult = serializer.Deserialize(dataToVerify!)
    print("-----------------------------------")
    

}

if Process.arguments.count < 3 {
    PrintHelp();
}
else {
    let source = Process.arguments[1]
    let dest = Process.arguments[2]

    //let source = "/Users/thomas/Work/Project-Cube/Source/src/bin/"
    //slet dest = "/Users/thomas/Work/Project-Cube/Source/src/bin/"
    print("Source path: " + source)
    print("Destination path: " + dest)
    
    // Create result directory
    do {
        try NSFileManager.defaultManager().createDirectoryAtPath(dest, withIntermediateDirectories: false, attributes: nil)
    } catch let error as NSError {
        print(error.localizedDescription);
    }
    
    // Iterate through GIFs, put the converter result into -> /result/
    let fileManager = NSFileManager.defaultManager()
    let enumerator:NSDirectoryEnumerator = fileManager.enumeratorAtPath(source)!
    
    print("========= CONVERTING FILE(S) =========")
    while let element = enumerator.nextObject() as? String {
        if element.hasSuffix("gif") {
            let destUrl = dest + (NSURL(fileURLWithPath: element).URLByDeletingPathExtension?.lastPathComponent!)! + ".pmz"
            convertFile(source + element, dest: destUrl)
        }
    }
    print("Conversion Finished.")
}






