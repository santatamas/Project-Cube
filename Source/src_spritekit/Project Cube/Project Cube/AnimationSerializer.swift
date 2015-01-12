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
//        var path = NSURL.fileURLWithPath("/Users/../tesData/test.zip")
//        var archive: ZZArchive = ZZArchive(URL:(fileURLWithPath:path!), error: nil)    
        
        var filePath:NSString = NSString(string: "")
        var data:NSData? = NSFileManager.defaultManager().contentsAtPath(filePath)
        
        var unzippedData:NSData? = data?.gunzippedData()
    }
    
    
}
