﻿# Блокировка

Исключительная блокировка используется, чтобы гарантировать, что только один поток может одновременно вводить определенные разделы кода. <br/> 
Двумя основными исключительными блокирующими конструкциями являются **lock** и **Mutex**. <br/>
Из двух **lock** конструкция более быстрая и удобная. <br/>
**Mutex** тем не менее, он имеет свою нишу в том, что его блокировка может охватывать приложения в различных процессах на компьютере.

## Сравнение блокирующих конструкций

**lock (Monitor.Enter / Monitor.Exit)** _20 нс_

**Mutex** _1000 нс_ (Кросс-процесс? - да)

    Гарантирует, что только один поток может получить доступ к ресурсу или разделу кода за раз

**SemaphoreSlim** (введено в Framework 4.0) _200 нс_

**Semaphore** _1000 нс_ (Кросс-процесс? - да)

    Гарантирует, что не более указанного количества одновременных потоков могут получить доступ к ресурсу или разделу кода.

**ReaderWriterLockSlim** (введено в Framework 3.5) _40 нс_

**ReaderWriterLock** (устарело)_100 нс_

    Позволяет нескольким читателям сосуществовать с одним писателем