using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web.UI.WebControls;
using JudgeCodeRunner;
using OnlineJudge.Repository;

namespace OnlineJudge.Models {

    class ProblemDummyData{
public static string intput= @"1000
47 48
24 61
47 58
87 43
33 13
96 76
28 9
75 29
1 32
11 30
98 78
33 27
90 35
34 11
94 45
21 15
30 35
11 19
38 11
40 73
99 65
17 57
58 85
49 41
85 5
95 38
97 53
60 58
27 24
26 43
87 9
63 32
7 93
15 29
28 65
31 43
59 11
80 85
77 93
34 61
32 5
19 5
59 31
93 17
12 3
24 95
47 88
8 7
93 36
14 51
85 98
63 43
55 8
46 81
23 17
99 43
44 99
95 32
97 24
38 15
42 8
70 30
50 95
35 90
15 87
91 71
2 43
78 86
83 5
63 86
98 6
77 43
87 65
39 54
87 65
72 63
83 65
32 36
54 57
85 10
40 6
17 71
30 71
53 72
46 59
21 12
91 62
38 3
20 49
70 10
85 40
49 77
43 89
98 57
26 30
18 27
52 52
9 82
49 78
30 30
49 79
91 74
42 10
32 80
80 45
97 12
40 91
60 56
74 38
73 79
2 94
4 65
94 49
45 86
89 92
22 67
54 65
56 51
99 7
78 6
66 59
74 71
27 66
18 72
5 87
20 63
80 57
76 30
43 14
82 37
49 51
38 80
81 25
85 64
40 92
10 36
7 20
51 30
74 66
83 4
44 21
98 24
14 69
6 31
78 66
82 39
22 71
54 32
86 87
79 13
35 82
21 90
42 62
57 47
28 14
83 24
36 30
12 96
50 98
37 59
13 66
85 73
74 83
34 68
84 54
38 92
80 70
21 8
92 50
6 76
79 45
93 70
19 51
36 6
98 61
97 67
29 32
98 51
98 78
90 92
44 71
58 60
24 13
70 85
43 54
68 34
19 19
50 51
63 9
75 67
51 15
19 5
49 13
83 14
75 64
19 89
34 41
72 98
80 13
88 23
83 13
82 84
98 37
48 59
33 21
74 17
52 71
53 16
84 58
41 60
88 76
9 45
15 51
29 24
91 13
0 68
50 79
74 94
60 46
63 70
77 75
82 93
98 62
40 93
20 78
31 79
63 30
95 20
21 73
29 21
36 51
87 14
23 41
79 10
58 72
28 97
79 14
73 82
50 88
22 5
20 49
89 8
75 7
98 65
65 38
80 12
56 83
46 58
60 34
57 71
69 88
48 46
12 74
0 1
5 2
73 90
54 58
74 10
14 64
21 94
59 49
84 4
25 95
76 81
93 68
67 1
28 0
49 92
1 46
59 2
14 8
68 52
95 48
90 81
95 79
22 12
31 94
73 68
46 62
69 4
66 28
20 66
99 39
79 40
48 9
36 43
70 27
25 76
67 4
98 79
37 57
84 51
30 76
24 66
32 38
63 1
94 75
70 20
54 32
91 81
5 37
92 17
75 94
51 23
11 49
9 56
43 86
97 83
12 62
79 49
77 99
33 60
33 80
75 36
14 79
18 3
17 38
39 66
71 41
31 24
56 67
70 78
71 78
41 91
96 36
60 14
27 90
42 21
15 47
88 0
45 9
2 87
26 89
91 76
70 60
78 71
72 10
55 48
7 65
25 16
52 58
14 91
18 77
72 5
10 82
41 8
85 65
2 50
77 74
98 99
66 99
77 25
78 13
97 81
3 69
38 32
19 74
80 17
26 33
85 70
76 33
72 32
90 80
9 85
81 72
27 20
94 9
34 74
59 53
57 43
25 58
9 29
5 93
29 57
79 76
33 57
32 44
75 70
39 76
1 98
42 6
90 64
87 44
27 43
24 93
98 88
96 24
7 95
20 35
14 56
42 92
35 19
7 79
49 65
50 28
34 20
13 55
80 16
23 81
16 27
0 39
34 90
33 81
0 92
83 59
65 45
4 23
59 80
4 63
85 39
23 37
60 74
36 96
89 41
41 31
1 24
91 51
5 28
25 66
96 95
93 34
18 75
99 79
64 54
53 40
90 64
39 57
86 68
74 63
22 60
56 10
14 32
64 74
60 91
35 95
72 35
14 23
19 37
44 36
81 28
17 53
78 40
9 95
71 90
19 23
79 38
99 3
64 64
45 3
87 90
2 75
44 11
52 86
42 80
59 15
40 23
93 53
7 64
40 78
8 47
50 3
1 1
72 42
43 74
65 36
12 72
87 25
4 34
19 59
65 53
20 46
84 91
69 56
28 61
83 17
98 42
8 49
53 59
60 77
58 14
66 50
83 68
10 5
83 93
23 88
59 99
8 9
4 1
46 57
2 39
42 5
61 48
52 61
79 74
92 89
73 42
63 52
80 5
38 0
27 61
37 11
91 72
89 40
27 47
47 38
65 46
76 72
57 7
10 31
38 55
47 5
84 90
36 6
9 73
36 20
26 53
14 2
9 41
20 9
43 82
39 30
95 8
14 58
28 71
5 49
93 75
74 4
22 61
21 63
52 34
28 43
70 19
6 33
67 43
98 48
50 92
43 61
83 30
7 32
52 55
36 88
77 38
68 69
39 55
63 98
59 18
60 0
28 10
23 61
42 58
47 78
43 99
67 56
32 75
38 1
38 7
40 64
2 90
79 40
79 94
70 95
97 36
24 90
59 37
27 55
49 4
29 5
48 81
23 95
27 13
70 4
17 20
76 64
57 92
77 16
67 55
39 23
14 36
44 44
88 4
4 18
56 1
68 71
62 18
68 50
15 8
48 94
28 59
17 42
39 90
16 24
6 57
71 65
45 58
11 88
76 78
33 24
39 28
42 73
83 90
21 22
83 25
72 63
37 7
12 67
61 12
26 50
89 4
97 14
97 0
49 54
64 33
40 47
50 94
86 39
1 35
96 17
27 74
65 27
55 74
97 90
4 45
45 84
58 7
22 6
37 42
24 64
4 4
45 92
43 62
37 34
50 49
66 92
67 6
23 8
60 83
34 72
28 74
4 86
3 79
60 97
9 21
56 6
55 63
83 76
42 56
21 26
60 76
31 36
50 43
52 0
52 38
81 60
87 55
88 70
70 4
29 18
81 21
30 17
43 76
77 39
56 43
56 27
36 11
50 96
74 10
98 18
33 37
26 4
76 10
8 87
74 28
40 59
90 97
80 53
91 22
25 76
54 70
1 53
91 56
1 60
13 59
43 1
3 77
34 83
87 70
60 21
52 96
9 18
76 8
83 60
44 1
2 50
30 56
47 57
52 72
58 2
1 94
0 62
47 99
19 14
97 78
84 45
30 95
83 43
44 22
72 89
59 93
96 30
77 6
39 97
83 92
73 48
99 15
72 47
31 21
94 37
28 72
52 47
33 94
76 83
96 56
49 37
14 64
66 76
24 9
3 10
62 1
41 11
80 62
71 97
56 52
90 80
97 3
45 90
78 38
28 75
88 84
38 70
9 86
36 88
83 1
30 6
57 11
96 61
81 29
49 36
55 21
13 40
27 13
6 19
15 91
43 3
76 79
50 0
75 64
80 53
49 7
26 88
11 74
27 5
58 46
1 86
66 10
82 18
12 24
91 88
77 86
22 33
22 92
3 88
23 46
76 42
65 85
56 59
12 36
14 63
36 69
22 59
92 43
15 0
10 96
53 93
84 9
16 16
52 1
48 15
92 85
11 47
64 80
71 78
71 91
19 31
18 56
65 62
98 89
35 13
65 96
69 97
47 36
60 8
82 71
36 38
76 59
34 57
83 69
26 21
39 21
6 26
10 44
7 28
71 69
81 73
49 13
37 6
2 20
41 48
62 68
52 1
95 36
70 36
60 82
6 36
51 90
5 53
92 84
54 24
84 5
85 89
53 67
53 92
50 59
95 78
85 71
30 93
2 38
6 13
26 49
23 61
25 89
83 81
11 53
36 79
88 1
27 91
82 44
93 31
34 41
22 87
80 62
97 48
77 71
89 45
48 11
91 22
96 34
45 58
86 30
12 23
1 97
22 93
59 32
70 36
9 81
81 96
5 0
42 6
49 83
55 31
30 25
67 92
56 97
27 43
26 98
30 8
97 59
55 71
77 19
20 39
73 89
66 1
69 86
76 16
85 8
4 7
40 64
30 35
73 80
31 48
82 93
88 8
52 34
79 98
75 29
62 21
21 66
8 65
37 72
50 2
29 13
19 63
89 27
70 91
94 28
69 78
69 61
93 98
65 86
14 69
79 49
81 79
79 89
7 70
46 49
39 96
73 70
11 64
50 88
19 0
4 65
48 61
99 52
50 44
30 3
95 3
91 15
34 71
28 83
11 82
24 38
56 3
55 21
84 60
18 89
12 73
79 25
68 85
78 21
45 30
64 63
91 97
80 89
87 93
5 77
10 82
27 99
49 84
14 37
71 81
36 9
15 64
95 45
9 60
65 5
32 98
8 25
58 4
11 70
91 71
57 58
91 79
29 27
99 47
87 25
46 63
92 25
19 63
47 31
81 57
94 21
88 66
69 1
26 62
6 60
50 1
91 54
47 90
13 87
75 31
21 91
80 43
88 60
8 82
14 76
74 24
8 55
63 70
87 63
43 49
81 31
13 87
37 71
79 16
81 5
20 34
83 69
75 10
35 27
21 24
3 29";
        public static string expected_output = @"95
85
105
130
46
172
37
104
33
41
176
60
125
45
139
36
65
30
49
113
164
74
143
90
90
133
150
118
51
69
96
95
100
44
93
74
70
165
170
95
37
24
90
110
15
119
135
15
129
65
183
106
63
127
40
142
143
127
121
53
50
100
145
125
102
162
45
164
88
149
104
120
152
93
152
135
148
68
111
95
46
88
101
125
105
33
153
41
69
80
125
126
132
155
56
45
104
91
127
60
128
165
52
112
125
109
131
116
112
152
96
69
143
131
181
89
119
107
106
84
125
145
93
90
92
83
137
106
57
119
100
118
106
149
132
46
27
81
140
87
65
122
83
37
144
121
93
86
173
92
117
111
104
104
42
107
66
108
148
96
79
158
157
102
138
130
150
29
142
82
124
163
70
42
159
164
61
149
176
182
115
118
37
155
97
102
38
101
72
142
66
24
62
97
139
108
75
170
93
111
96
166
135
107
54
91
123
69
142
101
164
54
66
53
104
68
129
168
106
133
152
175
160
133
98
110
93
115
94
50
87
101
64
89
130
125
93
155
138
27
69
97
82
163
103
92
139
104
94
128
157
94
86
1
7
163
112
84
78
115
108
88
120
157
161
68
28
141
47
61
22
120
143
171
174
34
125
141
108
73
94
86
138
119
57
79
97
101
71
177
94
135
106
90
70
64
169
90
86
172
42
109
169
74
60
65
129
180
74
128
176
93
113
111
93
21
55
105
112
55
123
148
149
132
132
74
117
63
62
88
54
89
115
167
130
149
82
103
72
41
110
105
95
77
92
49
150
52
151
197
165
102
91
178
72
70
93
97
59
155
109
104
170
94
153
47
103
108
112
100
83
38
98
86
155
90
76
145
115
99
48
154
131
70
117
186
120
102
55
70
134
54
86
114
78
54
68
96
104
43
39
124
114
92
142
110
27
139
67
124
60
134
132
130
72
25
142
33
91
191
127
93
178
118
93
154
96
154
137
82
66
46
138
151
130
107
37
56
80
109
70
118
104
161
42
117
102
128
48
177
77
55
138
122
74
63
146
71
118
55
53
2
114
117
101
84
112
38
78
118
66
175
125
89
100
140
57
112
137
72
116
151
15
176
111
158
17
5
103
41
47
109
113
153
181
115
115
85
38
88
48
163
129
74
85
111
148
64
41
93
52
174
42
82
56
79
16
50
29
125
69
103
72
99
54
168
78
83
84
86
71
89
39
110
146
142
104
113
39
107
124
115
137
94
161
77
60
38
84
100
125
142
123
107
39
45
104
92
119
173
165
133
114
96
82
53
34
129
118
40
74
37
140
149
93
122
62
50
88
92
22
57
139
80
118
23
142
87
59
129
40
63
136
103
99
154
57
67
115
173
43
108
135
44
79
73
76
93
111
97
103
97
87
144
125
36
113
101
92
129
187
49
129
65
28
79
88
8
137
105
71
99
158
73
31
143
106
102
90
82
157
30
62
118
159
98
47
136
67
93
52
90
141
142
158
74
47
102
47
119
116
99
83
47
146
84
116
70
30
86
95
102
99
187
133
113
101
124
54
147
61
72
44
80
117
157
81
148
27
84
143
45
52
86
104
124
60
95
62
146
33
175
129
125
126
66
161
152
126
83
136
175
121
114
119
52
131
100
99
127
159
152
86
78
142
33
13
63
52
142
168
108
170
100
135
116
103
172
108
95
124
84
36
68
157
110
85
76
53
40
25
106
46
155
50
139
133
56
114
85
32
104
87
76
100
36
179
163
55
114
91
69
118
150
115
48
77
105
81
135
15
106
146
93
32
53
63
177
58
144
149
162
50
74
127
187
48
161
166
83
68
153
74
135
91
152
47
60
32
54
35
140
154
62
43
22
89
130
53
131
106
142
42
141
58
176
78
89
174
120
145
109
173
156
123
40
19
75
84
114
164
64
115
89
118
126
124
75
109
142
145
148
134
59
113
130
103
116
35
98
115
91
106
90
177
5
48
132
86
55
159
153
70
124
38
156
126
96
59
162
67
155
92
93
11
104
65
153
79
175
96
86
177
104
83
87
73
109
52
42
82
116
161
122
147
130
191
151
83
128
160
168
77
95
135
143
75
138
19
69
109
151
94
33
98
106
105
111
93
62
59
76
144
107
85
104
153
99
75
127
188
169
180
82
92
126
133
51
152
45
79
140
69
70
130
33
62
81
162
115
170
56
146
112
109
117
82
78
138
115
154
70
88
66
51
145
137
100
106
112
123
148
90
90
98
63
133
150
92
112
100
108
95
86
54
152
85
62
45
32";
    }
    
    
    public class SeedDataRepository {
        public static List<Problem> getProblems(OjDBContext ctx){
            var user = ctx.Users.First(x => x.UserName == "admin");
            var problems= new List<Problem>();

            for(int i = 1; i <= 30; i++){
                var a=  new Problem(){
                    Title = "Problem #"+ i,
                    TestCaseInput = ProblemDummyData.intput,
                    TestCaseOutput = ProblemDummyData.expected_output,
                    TimeLimit = 1,
                    MemoryLimit = 25,
                    CreateDate = DateTime.Now,
                    Creator = user
                };

                
                problems.Add(a);
            }
            

            return problems;
        }

        // only for develpment purposes
        public static List<Submission> getSubmissions(OjDBContext ctx){
            var array = new List<Submission>();

            var admin = ctx.Users.First(x => x.UserName == "admin");
            var problem = ctx.Problems.First();

//            for(int i = 1; i <= 40; i++){
//                array.Add(new Submission(){
//                    Submitter = admin,
//                    Problem = problem,
//                    ProgrammingLanguage = ctx.ProgrammingLanguages.Find(ProgrammingLanguageEnum.Cpp11),
//                    Status = ctx.SubmissionStatus.Find(Verdict.Accepted),
//                });
//            }
            

            return array;
        }

        // only for develpment purposes
        public static List<Announcement> getAnnouncements(OjDBContext ctx){
            var array = new List<Announcement>();

            var admin = ctx.Users.First(x => x.UserName == "admin");

            for(int i = 1; i <= 40; i++){
                array.Add(new Announcement(){
                    Title =  "Site Notice #"+i,
                    Description = "Notice description goes here",
                    CreateDate = DateTime.Now,
                    Creator = admin
                });
            }
            

            return array;
        }

        public static List<UserType> getUserTypes(){
            var types = new List<UserType>();

            types.Add(new UserType(){
                Id = UserTypeEnum.Admin,
                TypeName = "Admin"
            });

            types.Add(new UserType(){
                Id = UserTypeEnum.User,
                TypeName = "User"
            });

            types.Add(new UserType(){
                Id = UserTypeEnum.Judge,
                TypeName = "Judge"
            });

            return types;
        }

        public static List<User> getUsers(OjDBContext ctx){
            var users = new List<User>();
            var password = HashUtility.MD5Hash("password");
            users.Add(new User()
            {
                UserName = "admin",
                Email = "admin@admin.min",
                Password = password,
                UserType = ctx.UserTypes.Find(UserTypeEnum.Admin)
            });

            users.Add(new User()
            {
                UserName = "user",
                Email = "user@oj.com",
                Password = password,
                UserType = ctx.UserTypes.Find(UserTypeEnum.User)
            });

            users.Add(new User()
            {
                UserName = "judge",
                Email = "judge@oj.com",
                Password = password,
                UserType = ctx.UserTypes.Find(UserTypeEnum.Judge)
            });

            return users;
        }

        public static List<ProgrammingLanguage> GetProgrammingLanguages(){
            var seeds = new List<ProgrammingLanguage>();

            seeds.Add(new ProgrammingLanguage(){
                Id= ProgrammingLanguageEnum.C,
                Name = "C"
            });
            seeds.Add(new ProgrammingLanguage(){
                Id= ProgrammingLanguageEnum.Cpp89,
                Name = "C++ (98)"
            });
            seeds.Add(new ProgrammingLanguage(){
                Id= ProgrammingLanguageEnum.Cpp11,
                Name = "C++ (11)"
            });
            seeds.Add(new ProgrammingLanguage(){
                Id= ProgrammingLanguageEnum.Python3,
                Name = "Python 3"
            });

            return seeds;
        }

        public static List<Contest> GetContests(OjDBContext ctx){
            var past = DateTime.Now;
            past = past.Subtract(TimeSpan.FromHours(5));

            var present = DateTime.Now;

            var future = DateTime.Now;
            future = future.AddHours(5);


            return new List<Contest>(){
                CreateContes(ctx, "Contest 1", past, present),   // past contest
                CreateContes(ctx, "Contest 2", past, future),   // running contest
                CreateContes(ctx, "Contest 3", future, DateTime.MaxValue),   // upcomping contest
            };
        }

        // create one cotnest entry using given start and end_time dates
        public static Contest CreateContes(OjDBContext ctx, string title, DateTime start_time, DateTime end_time){
            Problem problem = ctx.Problems.First();

            var contest = new Contest(){
                Title = title,
                Description = "Contest Contest",
                Creator = ctx.Users.First(),

                StartDate = start_time,
                EndDate = end_time,
            };


            for (int i = 0; i < 5; i++){
                ContestProblem contest_problem = new ContestProblem(){
                    Order = i,
                    Contest = contest,
                    Problem =  problem,
                };
                contest.Problems.Add(contest_problem);
            }

            
            for (int i = 0; i < 30; i++){
                Contestant contestant = new Contestant(ctx.Users.First());
                contest.Contestants.Add(contestant);
            }

            return contest;
        }
    }
}