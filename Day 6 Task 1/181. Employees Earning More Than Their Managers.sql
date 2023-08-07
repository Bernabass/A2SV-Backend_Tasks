SELECT a.name AS Employee
FROM employee a
JOIN employee b
ON b.id = a.managerId
WHERE a.salary > b.salary;
