using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public interface IGetter
{
    string GetName();
    object GetValue(object target);
}
public class GetterField : IGetter
{
    FieldInfo f;
    public GetterField(FieldInfo f) { this.f = f; }
    public string GetName() { return f.Name; }
    public object GetValue(object target)
    {
        return f.GetValue(target);
    }
}
public class GetterMethod : IGetter
{
    MethodInfo m;
    public GetterMethod(MethodInfo m) { this.m = m; }
    public string GetName() { return m.Name; }
    public object GetValue(object target)
    {
        return m.Invoke(target, new object[0]);
    }
}

public class GetterProperty : IGetter
{
    PropertyInfo p;
    public GetterProperty(PropertyInfo p) { this.p = p; }
    public string GetName() { return p.Name; }
    public object GetValue(object target)
    {
        return p.GetValue(target);
    }
}

public class Logger
{

    private List<IGetter> toPrint;


    public Logger()
    {
        toPrint = new List<IGetter>();
    }

    public void ReadFields() { }

    public void ReadMethods() { }

    public void ReadProperties() { }


    public void Log(object o)
    {
        Type t = o.GetType();
        if (t.IsArray) LogArray((IEnumerable)o);
        else
        {
            var fs = InitFields(t); // 1x
            var getters = InitMethods(t); // 1x
            var props = InitProperties(t);
            getters.AddRange(fs);
            getters.AddRange(props);
            LogObject(o, getters);
        }
    }

    public  IEnumerable<IGetter> InitFields(Type t)
    {
        List<IGetter> l = new List<IGetter>();
        foreach (FieldInfo m in t.GetFields())
        {
            l.Add(new GetterField(m));
        }
        return l;
    }
    public  List<IGetter> InitMethods(Type t)
    {
        List<IGetter> l = new List<IGetter>();
        foreach (MethodInfo m in t.GetMethods())
        {
            if (m.ReturnType != typeof(void) && m.GetParameters().Length == 0)
            {
                l.Add(new GetterMethod(m));
            }
        }
        return l;
    }

    public  IEnumerable<IGetter> InitProperties(Type t)
    {
        List<IGetter> l = new List<IGetter>();
        foreach (PropertyInfo p in t.GetProperties())
        {
            l.Add(new GetterProperty(p));
        }
        return l;
    }

    public  void LogArray(IEnumerable o)
    {
        Type elemType = o.GetType().GetElementType(); // Tipo dos elementos do Array
        var fs = InitFields(elemType); // 1x
        var getters = InitMethods(elemType); // 1x
        var properties = InitProperties(elemType);
        getters.AddRange(fs);
        //getters.AddRange(properties);
        Console.WriteLine("Array of " + elemType.Name + "[");
        foreach (object item in o) LogObject(item, getters); // * 
        Console.WriteLine("]");
    }

    public  void LogObject(object o, IEnumerable<IGetter> gs)
    {
        Type t = o.GetType();
        Console.Write(t.Name + "{");
        foreach (IGetter g in gs)
        {
            Console.Write(g.GetName() + ": ");
            Console.Write(g.GetValue(o) + ", ");
        }
        Console.WriteLine("}");
    }
}