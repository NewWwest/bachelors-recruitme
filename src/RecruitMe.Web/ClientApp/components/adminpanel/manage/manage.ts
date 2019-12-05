import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';
import { SystemEntity } from '../../../models/administraion.models';

@Component({})
export default class ManageComponent extends Vue {
    SystemEntityEnum = SystemEntity;
    currentSystemEntity: SystemEntity = SystemEntity.Candidate;
    items: any = [];
    pagination = {
        rowsPerPage: 5,
        page: 1
    }

    headers() {
        switch (this.currentSystemEntity) {
            case SystemEntity.Candidate:
                return [
                    {
                        text: 'ID kandydata',
                        value: 'candidateId'
                    },
                    {
                        text: 'Imię',
                        value: 'name'
                    },
                    {
                        text: 'Nazwisko',
                        value: 'surname'
                    },
                    {
                        text: 'Email',
                        value: 'email'
                    }
                ];
            case SystemEntity.Exam:
                return [
                    {
                        text: 'ID egzaminu',
                        value: 'id'
                    },
                    {
                        text: 'Nazwa',
                        value: 'name'
                    },
                    {
                        text: 'Data',
                        value: 'startDateTime'
                    },
                    {
                        text: 'Kategoria',
                        value: 'examCategory.name'
                    }
                ];
            case SystemEntity.ExamCategory:
                return [
                    {
                        text: 'ID Kategorii',
                        value: 'id'
                    },
                    {
                        text: 'Nazwa',
                        value: 'name'
                    }
                ];
            case SystemEntity.Teacher:
                return [
                    {
                        text: 'ID nauczyciela',
                        value: 'id'
                    },
                    {
                        text: 'Imię',
                        value: 'name'
                    },
                    {
                        text: 'Nazwisko',
                        value: 'surname'
                    }
                ];
        }
    }

    @Watch('pagination')
    onPropertyChanged(value: any, oldValue: any) {
        this.fetchItems(this.currentSystemEntity, value)
    }
    mounted() {
        let type = this.$route.params.entityType;
        if (type == SystemEntity.Candidate ||
            type == SystemEntity.Exam ||
            type == SystemEntity.ExamCategory ||
            type == SystemEntity.Teacher) {
            this.currentSystemEntity = type;
            this.fetchItems(type, this.pagination);
        }
        else {
            this.$router.push(`/adminPanel/manage/${SystemEntity.Candidate}`);
        }
    }

    changeEntity(ent: SystemEntity) {
        this.currentSystemEntity = ent;
        this.$router.push(`/adminPanel/manage/${ent}`);
        this.fetchItems(this.currentSystemEntity, this.pagination);
    }

    handleSelect(entity: any) {
        this.$router.push(`/adminPanel/details/${this.currentSystemEntity}/${entity.id}`);
    }

    fetchItems(ent: SystemEntity, pagingParams: any) {
        console.error("TODO: fetch items from DB")
        this.items =
            [
                {
                    id: 1,
                    name: 'Matma1',
                },
                {
                    id: 2,
                    name: 'Matma2',
                },
                {
                    id: 3,
                    name: 'Matma3',
                },
                {
                    id: 4,
                    name: 'Matma4',
                },
                {
                    id: 5,
                    name: 'Matma5',
                },
                {
                    id: 6,
                    name: 'Matma6',
                },
                {
                    id: 7,
                    name: 'Matma7',
                },
                {
                    id: 8,
                    name: 'Matma8',
                },
                {
                    id: 9,
                    name: 'Matma9',
                },
                {
                    id: 11,
                    name: 'Matma11',
                },
                {
                    id: 12,
                    name: 'Matma12',
                },
                {
                    id: 13,
                    name: 'Matma13',
                },
            ];
    }
}
