using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// loader 只是上attachment 每个attachment 对应一个assets
/// 如果已经attach了一个a，要attachment一个b，只有在b加载完了，才会释放a,避免加载过程中出现空白。
/// 比如说一张image，一开始是一张Sprite A，然后要替换另一张Sprite B,
/// 按照YooAsset的逻辑来看,只有释放Sprite的Handle，才能释放掉这张sprite，但在ui框架中，只有view释放的时候Handle才会被释放，
/// 但如果有Attach这一层，可能会及时释放，但会不会有问题呢？ 
/// </summary>
public class Attachment<T> where T : UnityEngine.Object
{
    
    
}