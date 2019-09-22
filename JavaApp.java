package pt.isel.ls.utils;

import java.io.File;
import java.lang.reflect.Type;

class A {}
class B extends A {}
class C extends B {
    public int x, y;
    public void Foo() {}
}

public class JavaApp{

    public static void main(String[] args) {
        printBaseTypes("Ola");
        printBaseTypes(19);
        printBaseTypes(new C());
        printBaseTypes(new File("."));
//java.io.D
        printMembers(new C());
        printMethods(new C());
        printFields(new C());

        PrintInterfaces("Ola".getClass());
        PrintInterfaces(((Integer)(19)).getClass());
        PrintInterfaces((new C()).getClass());
        PrintInterfaces((new File(".")).getClass());
    }


    public static void printMembers(Object obj) {
        System.out.print("Members: ");
        for (var m: obj.getClass().getNestMembers())
            System.out.print(m.getName() + " ");
        System.out.println();
    }
    public static void printMethods(Object obj) {
        System.out.print("Methods: ");
        for (var m: obj.getClass().getMethods()) {
            System.out.print(m.getName() + " ");
        }
        System.out.println();
    }
    public static void printFields(Object obj) {
        System.out.print("Fields: ");
            for (var m: obj.getClass().getFields())
                System.out.print(m.getName() + " ");
        System.out.println();
    }


    public static void printBaseTypes(Object obj){

        Type t = obj.getClass();

        do {
            System.out.println(t.getTypeName()+ " ");
            PrintInterfaces(t);
            t = t.getClass().getSuperclass();

        } while( !(t instanceof Object));
        System.out.println();

    }

    public static void PrintInterfaces(Type t) {

        System.out.print("Interfaces: ");
        for (var m: t.getClass().getInterfaces())
            System.out.print(m.getName() + " ");
        System.out.println();

    }
}