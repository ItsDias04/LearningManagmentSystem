import { Component, inject } from '@angular/core';
import { SubjectForTutor } from '../../data/interfaces/subjects.interfaces';

import { ProfileService } from '../../data/services/profile.service';

@Component({
  selector: 'app-tutor-subjects',
  imports: [],
  templateUrl: './tutor-subjects.component.html',
  styleUrl: './tutor-subjects.component.css'
})
export class TutorSubjectsComponent {
  subjects: SubjectForTutor[] = [];
  service = inject(ProfileService);

  constructor () {
    this.service.getSubjectsForTutor().subscribe(
      (res) => {
        this.subjects = res
      });
  }
}
