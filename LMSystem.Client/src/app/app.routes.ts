import { Routes } from '@angular/router';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { canActivateAuth } from './auth/access.guard';
import { AppComponent } from './app.component';
import { TutorProfileComponent } from './pages/tutor-profile/tutor-profile.component';
import { NavigateBarComponent } from './common-ui/navigate-bar/navigate-bar.component';
import { StudentProfileComponent } from './pages/student-profile/student-profile.component';
import { StudentSubjectsComponent } from './pages/student-subjects/student-subjects.component';
import { GradesStudentComponent } from './pages/grades-student/grades-student.component';
import { TutorSubjectsComponent } from './pages/tutor-subjects/tutor-subjects.component';
import { TutorGradesComponent } from './pages/tutor-grades/tutor-grades.component';
import { TutorGradesSubComponent } from './pages/tutor-grades-sub/tutor-grades-sub.component';
import { TutorSearchComponent } from './pages/tutor-search/tutor-search.component';

export const routes: Routes = [
  {
    path: '',
    component: NavigateBarComponent,
    children: [
      { path: 'student/profile', component: StudentProfileComponent, canActivate: [() => canActivateAuth(['student'])] },
      {path: 'student/subjects', component: StudentSubjectsComponent, canActivate: [() => canActivateAuth(['student'])] },
      {path: 'student/grades', component: GradesStudentComponent, canActivate: [() => canActivateAuth(['student'])] },
      { path: 'tutor/profile', component: TutorProfileComponent, canActivate: [() => canActivateAuth(['tutor'])] },
      { path: 'tutor/subjects', component: TutorSubjectsComponent, canActivate: [() => canActivateAuth(['tutor'])] },
      { path: 'tutor/subjectsGrades', component: TutorGradesSubComponent, canActivate: [() => canActivateAuth(['tutor'])] },
      {path: 'tutor/grades', component: TutorGradesComponent, canActivate: [() => canActivateAuth(['tutor'])] },
      {path: 'tutor/search', component: TutorSearchComponent, canActivate: [() => canActivateAuth(['tutor'])] },
    ],
    canActivate: [() => canActivateAuth(['student', 'tutor'])], 
  },
  { path: 'login', component: LoginPageComponent },
];