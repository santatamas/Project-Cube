package com.cube.data;

import java.io.IOException;

import com.badlogic.gdx.assets.AssetDescriptor;
import com.badlogic.gdx.assets.AssetManager;
import com.badlogic.gdx.assets.loaders.AsynchronousAssetLoader;
import com.badlogic.gdx.assets.loaders.FileHandleResolver;
import com.badlogic.gdx.files.FileHandle;
import com.badlogic.gdx.utils.Array;

public class AnimationLoader extends AsynchronousAssetLoader<Animation, AnimationParameter> {
	
	Animation animation;
	
	public AnimationLoader (FileHandleResolver resolver) {
		super(resolver);
	}

	@Override
	public void loadAsync (AssetManager manager, String fileName, FileHandle file, AnimationParameter parameter) {
		try {
			animation = AnimationSerializer.Deserialize(file);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	@Override
	public Animation loadSync (AssetManager manager, String fileName, FileHandle file, AnimationParameter parameter) {
		return animation;
	}

	@Override
	public Array<AssetDescriptor> getDependencies (String fileName, FileHandle file, AnimationParameter parameter) {
		return null;
	}
}