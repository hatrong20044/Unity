using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IsState<T> 
{
    void OnEnter(T t);//hiểu nôm na là xử lí,triển khai các hành động khi bd trạng thái mới như khơir tạo các biến or thực hiện các hành động khởi đầu 
    void OnExecute(T t);//thuc hien cac hanh dong lien tuc trong qua trinh duy tri tt
    void OnExit(T t);//thuc hien cac hanh dong truoc khi thoat tt htai
     
    
}
