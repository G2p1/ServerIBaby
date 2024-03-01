CREATE TABLE users (
  id integer PRIMARY KEY identity(1,1) ,
  username varchar(255),
  usr_password varchar(255),
  usr_role varchar(255),
  created_at datetime,
  subscribe_start datetime,
  sub_start_math_month datetime,
  sub_start_math_reading_month datetime,
  sub_start_reading_month datetime,
  sub_start_ready_wright_month datetime
);

CREATE TABLE year_gradation (
  id integer PRIMARY KEY identity(1,1),
  year_name varchar(255)
);

CREATE TABLE subjects (
  id integer PRIMARY KEY identity(1,1),
  subject_name varchar(255)
);

CREATE TABLE lessons (
  id integer PRIMARY KEY identity(1,1),
  year_gradation_id integer,
  less_week integer,
  subject_id integer,
  lesson_name varchar(255),
  less_query varchar(255),
  url_query varchar(255)
);

CREATE TABLE user_lesson_done (
  usr_id integer,
  lesson_id integer
);

ALTER TABLE lessons ADD FOREIGN KEY (subject_id) REFERENCES subjects (id);

ALTER TABLE lessons ADD FOREIGN KEY (year_gradation_id) REFERENCES year_gradation (id);

ALTER TABLE user_lesson_done ADD FOREIGN KEY (usr_id) REFERENCES users (id);

ALTER TABLE user_lesson_done ADD FOREIGN KEY (lesson_id) REFERENCES lessons (id);

insert into subjects(subject_name)
values('math');
insert into subjects(subject_name)
values('reading');
insert into subjects(subject_name)
values('writing');


insert into year_gradation(year_name)
values('pre-school');
insert into year_gradation(year_name)
values('first-year');
insert into year_gradation(year_name)
values('second-year');
insert into year_gradation(year_name)
values('third-year');
insert into year_gradation(year_name)
values('fourth-years');

insert into lessons(year_gradation_id, less_week, subject_id, lesson_name, less_query, url_query)
values( 1, 2, 1, 'reading babi', '1212', 'https://www.youtube.com/embed/B1J6Ou4q8vE;;https://www.youtube.com/embed/B1J6Ou4q8vE;');
insert into lessons(year_gradation_id, less_week, subject_id, lesson_name, less_query, url_query)
values( 1, 2, 1, 'learn adding', '1212', 'https://www.youtube.com/embed/B1J6Ou4q8vE;;https://www.youtube.com/embed/B1J6Ou4q8vE;');
insert into lessons(year_gradation_id, less_week, subject_id, lesson_name, less_query, url_query)
values( 1, 3, 1, 'learn poping', '1212', 'https://www.youtube.com/embed/B1J6Ou4q8vE;;https://www.youtube.com/embed/B1J6Ou4q8vE;');
