FROM mysql
ADD ./seeds /docker-entrypoint-initdb.d/
CMD /etc/init.d/mysql start && mysql -u root sys -p${MYSQL_ROOT_PASSWORD} -host 0.0.0.0 -port 3306 < /docker-entrypoint-initdb.d/