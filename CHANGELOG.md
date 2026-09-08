# [1.7.0](https://github.com/alimertcetin/XIV/compare/v1.6.0...v1.7.0) (2026-09-08)


### Bug Fixes

* **.asmdef.meta:** Well, I'll just keep these. ([d48b8a2](https://github.com/alimertcetin/XIV/commit/d48b8a22bdd8c396cda3f497389fb7423e70b31f))
* **PoissonDiscSampler:** Early return if gridWidth or gridHeight are invalid ([773ad3c](https://github.com/alimertcetin/XIV/commit/773ad3cf848bc1366f7870d7efc8cd8338a388a7))
* **TypeExtensions.GetFieldOrPropertyValue:** Throws exception if propertyInfo is null ([67cfdef](https://github.com/alimertcetin/XIV/commit/67cfdefadb1ecb0e94ef28e2885b5dbe6b84f455))
* **XIVPoolSystem:** Specify initialization option ([c0aa647](https://github.com/alimertcetin/XIV/commit/c0aa647c193999b9952606307b10889b37005d58))
* **XIVRandom:** InitState now recreates Random instance ([f5ad8d3](https://github.com/alimertcetin/XIV/commit/f5ad8d30984f106406c13b565068d709630d1065))


### Features

* Add PerlinNosise1d, PerlinNosise2d and PoissonDiscSampler ([d6d0804](https://github.com/alimertcetin/XIV/commit/d6d080474a11e9522bbef0a754b1bba096cd0ec6))
* Add TypeExtensions ([fdf55d6](https://github.com/alimertcetin/XIV/commit/fdf55d61b6f310199f2ecd1940ecf9b30b7ad177))
* Add XIVQuaternion ([09782e8](https://github.com/alimertcetin/XIV/commit/09782e81f8361a2a5b5fe684c20f01ab7d93b7c2))
* **ArrayExtensions.XIVGetClosest:** Add excludeArr with length ([0b59143](https://github.com/alimertcetin/XIV/commit/0b5914388bd8dafb7eac9900780f9b4308f7ca97))
* **ArrayExtensions:** Add FilterBy overload ([63be22e](https://github.com/alimertcetin/XIV/commit/63be22eb00fe78d5182b1e218dae53901decacf8))
* **ArrayExtensions:** Add GetClosest ([c3da5f6](https://github.com/alimertcetin/XIV/commit/c3da5f6cbb84c3d06b79bf61567ba45a3d2ef03f))
* **ArrayExtensions:** Add XIVContains overload ([d39e45e](https://github.com/alimertcetin/XIV/commit/d39e45e80cdc63f804f0e2d519224c5e3e88f383))
* **ArrayExtensions:** Add XIVGetClosest and XIVIndexOf ([90824ac](https://github.com/alimertcetin/XIV/commit/90824ac67da64ead6316ccd6e6a3057b4f226a33))
* **ArrayUtils.GetBuffer:** Remove out parameter ([65d23c7](https://github.com/alimertcetin/XIV/commit/65d23c73acab4f602e91a8976c5b85bda7903ce8))
* **ArrayUtils:** Add GetBuffer ([ffab804](https://github.com/alimertcetin/XIV/commit/ffab8048cef8d933f2df022a27ce323784f538e0))
* **DynamicArray:** Add ForEach function and RemoveAll overload ([672ba0c](https://github.com/alimertcetin/XIV/commit/672ba0ca893d2f69bd2318978ad730738b07b69f))
* **DynamicArray:** Add IncreaseCapacity and RemoveLast ([c58e845](https://github.com/alimertcetin/XIV/commit/c58e845e7f2beb15c9af5caf9a264d41d9914d5c))
* **DynamicArray:** override ToString to display count ([84b44a8](https://github.com/alimertcetin/XIV/commit/84b44a85cb6f6519c66b3cf18e00e1dd33efca5a))
* **IListExtensions:** Add XIVPickRandom overload ([5c89496](https://github.com/alimertcetin/XIV/commit/5c89496560b000930d0eb864b6b04388ebc889ab))
* LineMath: Add IsIntersect to check intersections ([42551b0](https://github.com/alimertcetin/XIV/commit/42551b00420ad35faf5137aef86e17e6ac6e2fae))
* **PerlinNoise:** Adapt new GetBuffer function ([2f17d35](https://github.com/alimertcetin/XIV/commit/2f17d3509ee24caaaf07078202a89803c7fe70f2))
* **PoissonDiscSample:** Adapt new GetBuffer function ([fe56f9f](https://github.com/alimertcetin/XIV/commit/fe56f9f17fa9158dc90ecead9e2dcf897221969b))
* Remove implicit conversions for Unity ([2e9da4a](https://github.com/alimertcetin/XIV/commit/2e9da4a2103a482f54241da5d7c8f5c15a7ae601))
* **Timer:** Remove unity related stuff ([5f9f369](https://github.com/alimertcetin/XIV/commit/5f9f3698f54d7763463d047cbfdfae50d624525a))
* **TypeExtensions:** Add GetMembers ([d4d4494](https://github.com/alimertcetin/XIV/commit/d4d449472cf4c543a47fac3240fa429b1c964434))
* **Vec2:** Add Distance function to get distance between two vec2 ([be54e70](https://github.com/alimertcetin/XIV/commit/be54e708825d96e4985ee41df50362161effaa1c))
* **Vec2:** Add normalized field to get normalized Vec2 ([555530d](https://github.com/alimertcetin/XIV/commit/555530d1e5745b7331f5a6ab82b80247c0677744))
* **Vec2Extensions:** Add Vec2Extensions ([a69db42](https://github.com/alimertcetin/XIV/commit/a69db4281ac01f016ddcf0f0985e09add4156d02))
* **Vec2:** Negate operator ([eb6551b](https://github.com/alimertcetin/XIV/commit/eb6551b76c06e2b43d3f573a2ef52dddda137abd))
* **XIVBuffer:** Add XIVBuffer to dispose buffers without tracking ([e7503c3](https://github.com/alimertcetin/XIV/commit/e7503c3803fe5cdf67f4a9520aaa98155c691f38))
* **XIVBuffer:** Better Dispose, implicit array conversion, ref indexer ([4a586ea](https://github.com/alimertcetin/XIV/commit/4a586eabbf80bae8b545a638de55deb931991c3c))
* **XIVColor:** Add Lerp ([69002c4](https://github.com/alimertcetin/XIV/commit/69002c4060a13977b5ab1fcbe8542cf0158a2427))
* **XIVMathf.Max:** Use XIVMemory instead of params ([0a46899](https://github.com/alimertcetin/XIV/commit/0a46899ba42ef2617c492d2a287c234d9864e04e))
* **XIVMathf:** Add CeilToInt and FloorToInt ([a22b940](https://github.com/alimertcetin/XIV/commit/a22b940e534bddffbd841f667355df58b30e8a63))
* **XIVMathf:** Add Normalize function ([dafa031](https://github.com/alimertcetin/XIV/commit/dafa03148f9714131a3a46cbac573a59b9e8c0b4))
* **XIVMathInt:** Add Abs ([805078d](https://github.com/alimertcetin/XIV/commit/805078d8f77e2f0714ca2fa8017896902a4674f2))
* **XIVMathInt:** Add MethodImplOptions attribute ([38177bb](https://github.com/alimertcetin/XIV/commit/38177bb6a8a75294aeb4a6afce2de3d27ef1c1d6))
* **XIVMathInt:** Add NextPowerOfTwo function ([a72d3c9](https://github.com/alimertcetin/XIV/commit/a72d3c97b6a0539bfc3504219e1a90b69bed27c9))
* **XIVMemoryExtensions:** Add AsXIVMemory to convert XIVBuffer easily ([6649611](https://github.com/alimertcetin/XIV/commit/6649611f74cc7c3a68e96d4c3e3c0f258a9b7118))
* **XIVMemoryExtensions:** Add Fill function ([f790cc9](https://github.com/alimertcetin/XIV/commit/f790cc9733a3a09e53d319d6142d0ff4037ce3e5))
* **XIVMemoryExtensions:** Add Fill function to fill array with the giving values ([718f946](https://github.com/alimertcetin/XIV/commit/718f946c975b70e11f4ae48ae25f64f2e1f58479))
* **XIVMemoryExtensions:** Add FilterBy and AsXIVMemory overloads ([0e11734](https://github.com/alimertcetin/XIV/commit/0e11734fb470f992cc037dd993b1728ee835eaf2))
* **XIVMemoryExtensions:** Add GetClosest ([aa47363](https://github.com/alimertcetin/XIV/commit/aa47363456432adee4071c623a20a9a10e013fb7))
* **XIVMemory:** Keep track of array/list, IEnumerable<T> implementation ([b62cfc8](https://github.com/alimertcetin/XIV/commit/b62cfc8c068b3f6b919f8256fcdb9bba011f8fbd))
* **XIVRandom:** Add seed variable ([c9c63d0](https://github.com/alimertcetin/XIV/commit/c9c63d0074353ff8adae7b6025ec5ca00406ea00))


### Performance Improvements

* **XIVColor:** Make predefined colors readonly ([2b2363b](https://github.com/alimertcetin/XIV/commit/2b2363b11c0143542def136168c455419daebea1))

# [1.6.0](https://github.com/alimertcetin/XIV/compare/v1.5.0...v1.6.0) (2025-09-10)


### Features

* **IListExtensions:** Remove non-generic functions ([e5fd4b2](https://github.com/alimertcetin/XIV/commit/e5fd4b2cc322218897588d30b5fff591c4d57d1c))
* **XIVMemory:** Add new constructors and fix documentation ([409261d](https://github.com/alimertcetin/XIV/commit/409261d02db7861f2907ae6892e8634518339b32))
* **XIVMemoryExntesions:** FilterBy, PickWeighted, GetTotalWeight ([258e308](https://github.com/alimertcetin/XIV/commit/258e3082316d0c99e0554d4b969c6c90ce1fc1c2))

# [1.5.0](https://github.com/alimertcetin/XIV/compare/v1.4.1...v1.5.0) (2025-09-07)


### Features

* **ClassGenerator:** Add AddMemberArray function ([fa0c75a](https://github.com/alimertcetin/XIV/commit/fa0c75a8b86056725939c9b39d6b085ea82c73bd))

## [1.4.1](https://github.com/alimertcetin/XIV/compare/v1.4.0...v1.4.1) (2025-09-07)


### Bug Fixes

* **ClassGenerator:** OpenBracket and CloseBracket Update ([f5b487d](https://github.com/alimertcetin/XIV/commit/f5b487d65b5688b787676739d173de6fc8c84bae))

# [1.4.0](https://github.com/alimertcetin/XIV/compare/v1.3.1...v1.4.0) (2025-09-06)


### Features

* **Vec3:** Add -(Vec3) operator to reverse Vec3 ([9c6c6f5](https://github.com/alimertcetin/XIV/commit/9c6c6f525b0c551af726c1747b25aca36853c0c5))

## [1.3.1](https://github.com/alimertcetin/XIV/compare/v1.3.0...v1.3.1) (2025-08-27)


### Bug Fixes

* **BezierMath.CreateArc:** Lerp correctly ([476a3c4](https://github.com/alimertcetin/XIV/commit/476a3c4efec410cd1d3a6bfb815df0596093ec69))

# [1.3.0](https://github.com/alimertcetin/XIV/compare/v1.2.2...v1.3.0) (2025-08-20)


### Features

* **ArrayUtils:** Add Merge ([dd7f656](https://github.com/alimertcetin/XIV/commit/dd7f656ea50aa2b8203573428d324348f0cf3631))

## [1.2.2](https://github.com/alimertcetin/XIV/compare/v1.2.1...v1.2.2) (2025-08-20)


### Reverts

* Revert "fix(.gitignore): Add *.asmdef" ([37392ac](https://github.com/alimertcetin/XIV/commit/37392acada3c6bd14723fb2628a01fcbfad8608c))

## [1.2.1](https://github.com/alimertcetin/XIV/compare/v1.2.0...v1.2.1) (2025-08-20)


### Bug Fixes

* **.gitignore:** Add *.asmdef ([cf87ce6](https://github.com/alimertcetin/XIV/commit/cf87ce63773c9c9ef4cef3c5f042ae57f65ed732))

# [1.2.0](https://github.com/alimertcetin/XIV/compare/v1.1.0...v1.2.0) (2025-08-19)


### Features

* ***.asmdef:** Remove *.asmdef files ([808a4a5](https://github.com/alimertcetin/XIV/commit/808a4a576f9d597e8e096b894ca32f9b335736c4))

# [1.1.0](https://github.com/alimertcetin/XIV/compare/v1.0.1...v1.1.0) (2025-08-19)


### Features

* **.gitignore:** Untract .meta and asmdef files ([ccd230c](https://github.com/alimertcetin/XIV/commit/ccd230c11eb7be66379d7062db20f1299a42b98d))

## [1.0.1](https://github.com/alimertcetin/XIV/compare/v1.0.0...v1.0.1) (2025-07-31)


### Bug Fixes

* **package.json:** Change package name ([b32b85e](https://github.com/alimertcetin/XIV/commit/b32b85e113cb91e94230a982e379705f43ecfde2))

# 1.0.0 (2025-07-31)


### Bug Fixes

* **DynamicArray:** Consistency ([47ff695](https://github.com/alimertcetin/XIV/commit/47ff6954cd263013fe7839ce0877495a0e027e6b))
* **DynamicArray:** Proper Insert function ([bc4e6ae](https://github.com/alimertcetin/XIV/commit/bc4e6ae0ac40cb890c930e1d50f951c5dfc43dd0))
* **DynamicArray:** Proper range check on RemoveAt ([508c735](https://github.com/alimertcetin/XIV/commit/508c7356cf7602fdd146ccb25ffe50865f409111))
* **XIVDefaultEditor:** Validate target ([e0f6af1](https://github.com/alimertcetin/XIV/commit/e0f6af14063546a38568be971896b447f5b8eee9))
* **XIVMemory:** Fixed XIVMemory throws exception when length is 0 ([45df9bc](https://github.com/alimertcetin/XIV/commit/45df9bc0bff07426ccfb61862430fa4be35517be))
* **XIVTweenSystem:** Tweens are not cleaned up correctly. ([f4bfec8](https://github.com/alimertcetin/XIV/commit/f4bfec8ef8fe95896ae8d0f66f57cb2085dec489))


### Features

* Add AnimatorExtensions ([5e7e9db](https://github.com/alimertcetin/XIV/commit/5e7e9dbcdee6502f158b71cf1146fb44c616a218))
* Add ProfilerDefines and add Debug submenu to MenuItems ([93ed488](https://github.com/alimertcetin/XIV/commit/93ed488d0f53659b70ae03e7343504a6fa11a174))
* Add RaycastHitIListExtensions ([2948758](https://github.com/alimertcetin/XIV/commit/2948758fdad990c990ebbba868f99e1ad98158a3))
* Add TableBuillder ([8988c12](https://github.com/alimertcetin/XIV/commit/8988c128e19ed2a91c499556ca02956a38999af6))
* Add XIVMemory struct. Alternative to the Memory and Span ([b5478aa](https://github.com/alimertcetin/XIV/commit/b5478aa514ae319c2364063ed008e7d2cdc284a6))
* **AnimationConstantsGenerator:** Also write Parameter IDs ([aa29d63](https://github.com/alimertcetin/XIV/commit/aa29d63ea676e37463bb355d06e85e7bdb750fd9))
* **BezierMath:** Add CreateCurveNonAlloc method ([87978ee](https://github.com/alimertcetin/XIV/commit/87978eeb7faaed27bbe789e6bbfe2f28f41dac67))
* **BezierMath:** Add CurveData ([47d6bf9](https://github.com/alimertcetin/XIV/commit/47d6bf9ad3510578370069c527ae830b835da3e6))
* **BezierMath:** Added CreateArch function ([0b3fa56](https://github.com/alimertcetin/XIV/commit/0b3fa56e82b9a77c6a5f0399b32a883ab65d5150))
* **BezierMath:** CreateCurveNonAlloc accepts XIVMemory<Vec3> instead of Vec3[] ([3925404](https://github.com/alimertcetin/XIV/commit/3925404e7ac23578fcd82ed4ad56d08a56594a77))
* **DynamicArray:** Add AsReadOnlySpan ([80a1e11](https://github.com/alimertcetin/XIV/commit/80a1e11e9124d1226e9e29ee3c727b1c9b255104))
* **DynamicArray:** Add RemoveAll to mimic List<T> ([4db6676](https://github.com/alimertcetin/XIV/commit/4db66769fb6f997e32395890c2a9eb6be801cd97))
* **DynamicArray:** Add ToArray function ([0c93d3a](https://github.com/alimertcetin/XIV/commit/0c93d3ac66cd0ed492918e11491631b1326b9e1a))
* **DynamicArray:** Implement IList interface ([6b6c6c3](https://github.com/alimertcetin/XIV/commit/6b6c6c3dd85d7ff6441db499f2da56aca864bff3))
* **DynamicArray:** Use EqualityComparer<T>.Default ([1472289](https://github.com/alimertcetin/XIV/commit/14722895a5d60a3afc9dd2b04d20da84ff99120b))
* Remove .meta files and untrack them ([f7c96ad](https://github.com/alimertcetin/XIV/commit/f7c96ad17bce8136eac138f9eb5c140edced34c2))
* **StringExtensions:** Add PadCenter ([574f88e](https://github.com/alimertcetin/XIV/commit/574f88e90ecfbbb2e83001dd30c5bff5eb320b29))
* **StringExtensions:** Add TruncateWithChar ([d23ec2f](https://github.com/alimertcetin/XIV/commit/d23ec2f35570794e23406683282ce4d81278a538))
* **TableFormatters:** Comment out NewtonsoftJsonSerializer for now ([b41bf86](https://github.com/alimertcetin/XIV/commit/b41bf86e77b17f0c87ba1974eec79ddff84e8b5f))
* Update ClassGenerator for pretty print ([de0deca](https://github.com/alimertcetin/XIV/commit/de0decad20dc6a19b54d1db66d45543750620d19))
* **XIVMathInt:** Add Max ([aa3266c](https://github.com/alimertcetin/XIV/commit/aa3266c207d0edaf1982e7a657612748670362d8))
* **XIVMemory:** Add AsSpan() and ToArray() functions ([124b0da](https://github.com/alimertcetin/XIV/commit/124b0da32534d5293ae4e27d6f36ad57d54becb0))
* **XIVMemory:** Add IList support ([1d7e333](https://github.com/alimertcetin/XIV/commit/1d7e333abd8d4ca71db4fda63080ef6322049e5a))
* **XIVMemory:** Better exception explanation ([fb1cf4f](https://github.com/alimertcetin/XIV/commit/fb1cf4ff4b93cc11ed398d723bce90cb91478a38))
* **XIVMemory:** Check null array and bounds in constructor ([fd947fc](https://github.com/alimertcetin/XIV/commit/fd947fc12a30e86e2b71a6c3faa839702d755922))
* **XIVMemory:** Include isReversed for equality check ([792079a](https://github.com/alimertcetin/XIV/commit/792079af696ebd1561788b3f280aadba87e68efb))
* **XIVMemory:** Remove Reverse() ([8a88830](https://github.com/alimertcetin/XIV/commit/8a8883085c8684d967bca3f40f6a6c795f8fc265))
* **XIVMemory:** Slice will not dependent on underlying array ([6b238c9](https://github.com/alimertcetin/XIV/commit/6b238c9e8bd26ec9cfaca9f572200f6e4cd87c22))
* **XIVRandom:** Better random ([8fe17f7](https://github.com/alimertcetin/XIV/commit/8fe17f7e55f9ada09e092ba7cf714f4c38a3d9ce))
* **XIVTweenBuilder:** Throw cast error ([911b674](https://github.com/alimertcetin/XIV/commit/911b674b3c8a8048deebdf48f5c179545a8e9f26))
* **XIVTweenSystem:** Add very basic debugging ([7ed2afd](https://github.com/alimertcetin/XIV/commit/7ed2afd58b0b4a670e6180bfa3229f8faf97b75f))
* **XIVTweenSystem:** Debug->Display timestamps per tween ([d4c7959](https://github.com/alimertcetin/XIV/commit/d4c79597c6f209ddbe1762beb90d56c457b92dce))


### Performance Improvements

* **XIVTween:** Performance updates for XIVTween ([1b9a70b](https://github.com/alimertcetin/XIV/commit/1b9a70bf40d2a043f17631bcb1c14de7763cfbe7))

## [1.0.1](https://github.com/alimertcetin/UnityPackageTemplate/compare/v1.0.0...v1.0.1) (2023-11-04)


### Bug Fixes

* **documentation.yml:** Documentation workflow won't run on releases. ([938b6a2](https://github.com/alimertcetin/UnityPackageTemplate/commit/938b6a236f04082701c7c7f9ff613f2a53e5cbb8))

# 1.0.0 (2023-11-04)


### Bug Fixes

* Move package.json inside packageJson folder ([73d88ce](https://github.com/alimertcetin/UnityPackageTemplate/commit/73d88ce673ef2256c7e447101e00e430a54241ea))
