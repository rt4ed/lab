Мониторинг текстовых файлов в папке.

Необходимо разработать класс, который следит за текстовыми файлами в папке и может откатывать их к состоянию на определенную дату\время.

Необходимо реализовать интерфейс IFileMonitor в классе FileMonitor. Принцип работы:
1. В конструктор класса через интерфейс IConfiguration передается информация о том, за какой папкой нужно следить и в какой папке необходимо сохранять бэкапы.
2. Метод Start запускает отслеживание изменений во всех текстовых (*.txt) файлах в папке IConfiguration.Path
3. Метод Stop прекращает отслеживание изменений в текстовых файлах в папке IConfiguration.Path
4. Метод Reset осуществляет откат состояния текстовых файлов (их содержимое) на дату и время, переданные через параметр onDateTime 

Дополнительные требования и условия:
1. Возможностью изменения файлов в момент, когда не запущено отслеживание изменений, пренебречь.
2. При остановке работы монитора папка с бэкапами должна удаляться
3. Должна быть реализована возможность прекращения отслеживания изменений как при вызове метода Stop, так и при вызове метода Dispose, используя конструкцию using
4. Путь к папке с бэкапами оригинальных файлов задается с помощью IConfiguration.BackupPath (внутри неё можно хранить бэкапы как угодно, с любой глубиной вложенности, если потребуется)
5. В целях упрощения реализации принять, что одновременно может изменяться только один файл, и во время восстановления одного файла изменения в другие файлы не вносятся
6. Для отслеживания изменения файлов можно использовать класс FileSystemWatcher (https://docs.microsoft.com/ru-ru/dotnet/api/system.io.filesystemwatcher?view=net-5.0)