using UnityEngine;
using System.Collections.Generic;
using System;

public class Pool<T> {

    Stack<T> all;
    Func<T> function;
    int size;

    public Pool(Func<T> function, int size) {
        this.function = function;
        all = new Stack<T>(size);
        for (int i = 0; i < size; i++) {
            all.Push(function());
        }
    }

    public T Get() {
        if (all.Count == 0) {
            return function();
        }
        return all.Pop();
    }

    public void Return(T t) {
        all.Push(t);
    }

}