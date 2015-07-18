//
//  Author:
//    Chronium Silver (Andrei Dimitriu) onlivechronium@gmail.com
//
//  Copyright (c) 2015, Chronium @ ChronoStudios
//
//  All rights reserved.
//
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
// * Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.
//
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
using System;
using System.Collections.Generic;
using ChronosEngine.Base.Textures;

namespace ChronosEngine.Base {
	public abstract class Asset {

	}

	public abstract class AssetProvider<T> where T: Asset {
		public AssetProvider(string root, string assetRoot) {
			this.Root = root;
			this.AssetRoot = assetRoot;
		}

		public string Root { get; }
		public string AssetRoot { get; }

		public string GetAssetPath(string asset) {
			return Root + AssetRoot + asset;
		}

		public abstract T Load(string assetName, params object[] args);
	}

	public class ContentManager {
		public string ContentRoot { get; set; }

		public Dictionary<Type, object> AssetProviders = new Dictionary<Type, object>();

		public ContentManager(string root = "Assets/") {
			this.ContentRoot = root;
		}

		public void LoadPredefinedProviders() {
			this.RegisterAssetProvider<Texture2D>(typeof(Texture2DProvider));
		}

		public void RegisterAssetProvider<T>(Type type) {
			this.AssetProviders[typeof(T)] = Activator.CreateInstance(type, new[] { ContentRoot });
		}

		public T Load<T>(string asset, params object[] args) where T: Asset {
			if (AssetProviders.ContainsKey(typeof(T))) {
				AssetProvider<T> provider = (AssetProvider<T>)AssetProviders[typeof(T)];
                return provider.Load(provider.GetAssetPath(asset), args);
			}
			return null;
		}

		public T LoadAbsolute<T>(string asset, params object[] args) where T : Asset {
			if (AssetProviders.ContainsKey(typeof(T))) {
				AssetProvider<T> provider = (AssetProvider<T>)AssetProviders[typeof(T)];
				return provider.Load(asset, args);
			}
			throw new Exception("Unsupported " + typeof(T).Name +" asset type!");
		}
	}
}

