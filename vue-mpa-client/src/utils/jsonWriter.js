new Vue ({
    el: '#jsonWriter',
    data: {
        name: '',
        last: '',
        index: 0,
        grade: 0,
        arr: []
    },
  
    methods: {
        add: function (e) {
            this.arr.push({first: this.name, lastn: this.last, index: this.index, grade: this.grade});
            console.log(1);
        },
        saveFile: function() {
          const data = JSON.stringify(this.arr)
          window.localStorage.setItem('arr', data);
          console.log(JSON.parse(window.localStorage.getItem('arr')))
        }
    }
  });