//
//  TwoStateIcon.swift
//  Project Cube
//
//  Created by Tamas Santa on 15/01/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation
import SpriteKit

class TwoStateIcon: SKSpriteNode {
    
    private let _frameduration:Double = 2000
    private var _state1Texture: SKTexture
    private var _state2Texture: SKTexture
    private var _elapsedTime: Double = 0
    private var _state = false
    
    init(state1:NSString, state2:NSString) {
        _state1Texture = SKTexture(imageNamed: state1 as String)
        _state1Texture.filteringMode = SKTextureFilteringMode.Nearest
        
        _state2Texture = SKTexture(imageNamed: state2 as String)
        _state2Texture.filteringMode = SKTextureFilteringMode.Nearest
        
        super.init(texture: _state1Texture, color: UIColor.clearColor(), size: _state1Texture.size())
        var scaledSize = CGSizeMake(_state1Texture.size().width / 2, _state1Texture.size().height / 2)
        self.size = scaledSize
    }

    required init?(coder aDecoder: NSCoder) {
        fatalError("init(coder:) has not been implemented")
    }
    
    func SwitchStates() {
        if(_state) {
            texture = _state1Texture
        }
        else
        {
            texture = _state2Texture
        }
        _state = !_state
    }
    
    func Act(delta:Double) {
        if(_elapsedTime >= _frameduration) {
            _elapsedTime = 0
            SwitchStates();
        }
        _elapsedTime+=delta
    }
}