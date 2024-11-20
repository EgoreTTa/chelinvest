Проверочные задания для разработчика C
sharp

Задание 1.

Написать программу на C# для анализа и обработки данных из XML-файла

Описание задачи: необходимо написать программу, которая будет анализировать данные из предоставленного XML-файла и выполнять с ними определённые операции. Например, программа может подсчитывать количество элементов в файле, выводить их список или суммировать значения определённых атрибутов.

Требования к программе:

Программа должна принимать путь к файлу в качестве аргумента командной строки. Она должна корректно обрабатывать ошибки при открытии файла или при его анализе. В случае успешного выполнения программы она должна вывести результаты анализа в консоль. Для работы с XML можно использовать встроенные средства C#, такие как LINQ to XML или XmlDocument.

Проект **XMLAnalyzer**

_Примечания от автора:
  1. Аргументов может быть несколько. Каждый аргумент проверяется на валидность (путь к файлу это или нет).
  2. Анализируется несколько файлов, если были правильно указаны пути.
  3. Работает с относительными путями и полными.
  4. Файл не обязан быть *.xml формата, но обязан содержать XML тело.
  5. Анализируется все Elements включая Root.
  6. Выводится список всех Elements.
  7. Выводится кол-во уникальных Elements (выборка по имени).
  8. Выводятся атрибуты всех уникальных Elements._

Задание 2.

Написать приложение с помощью .NET Core, которое будет реализовывать работу с банковскими счетами. Приложение должно иметь следующие функциональности:
1. Возможность создания новых банковских счетов.
2. Возможность вывода баланса на счете.
3. Возможность перевода средств между счетами.
4. Возможность получать историю транзакций по счету.

Приложение должно иметь следующую архитектуру:
- Класс Account для представления банковского счета с его свойствами и методами.
- Класс Bank для представления банка и управления коллекцией счетов.
- Класс Program (.Net Core 3.1 не предлагает из коробки такой шаблон, использовал WPF приложение) для запуска приложения и предоставления пользовательского интерфейса.

Проект **BankSystem**
_Примечания от автора:
  1. Базы данных в рамках тестого проекта не делал. Все данные существуют в runtime.
  2. Позволяет создавать счета в рамках 1 банка (**TestBank**).
  3. При создании счета имена счетов могут повторятся. Содается с уникальным ID инкриминируя последний в списке.
  5. Проверяет поле суммы (может быть числом или нет).
  6. Нельзя создать счет без имени. // todo может стоит добавить генератор... Поле опциональное
  7. При выборе счета отображается баланс и список транзакций сортированный от новых к старым.
  8. При создании транкзакции нужно выбрать счет, куда хотите перевести (в выборке отсутсует текущий счет).
  9. Сумма перевода так же проверяется на число. Система не позволит совершить перевод суммы меньше ноля или ноля. Нельзя сделать перевод, если сумма перевода больше баланса на счету.
  10. При отработке транкзакции, история сразу обновляется в обоих счетах.
  11. История перевода привязана к текущему банку. Существует в рамках банка. Ссылается на ID счетов откуда и куда был выполнен перевод. Имеет поле даты совершения перевода и суммы перевода._

Общие требования к реализации:
1. Используйте .NET Core для разработки приложения.
2. Используйте C# как основной язык программирования.
3. Управляйте коллекцией счетов с помощью класса Bank .
4. Предоставьте пользовательский интерфейс для создания, вывода баланса и перевода средств между счетами.
6. Используйте ООП (Object-Oriented Programming) принципы для реализации.

Отдавайте приоритет следующим факторам:
1. Качество кода
2. Умение решать задачи с помощью C#
3. Понимание концепций программирования, таких как ООП и коллекции

Время на решение этой задачи не ограничено, но ожидается, что вы завершите работу за 1-2 дня.

Примечание:
- Приложение должно быть полностью функциональным.
- Документация должна быть четкой и понятной.
- Код должен быть читаемым и поддерживаемым

_Примечания от автора:
  1. Тестировалось на Windows 10 x64.
  2. Использовался .Net Core 3.1 с возможностью переноса на Linux системы.
  3. UI **BankSystem** не перенесется. //todo нужно было разделить UI реализацию от бекенда.
